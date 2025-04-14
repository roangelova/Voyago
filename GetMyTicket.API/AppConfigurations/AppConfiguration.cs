using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using GetMyTicket.Persistance.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GetMyTicket.API.AppConfigurations
{
    public static class AppConfiguration
    {
        public static async Task<WebApplicationBuilder> ConfigureApp(this WebApplicationBuilder builder)
        {
            string dbConnection = string.Empty;
            string JwtIssuer = string.Empty;
            string JwtAudience = string.Empty;
            string JwtKey = string.Empty;

            //GET VALUES BASED ON ENVIRONMENT 
            if (builder.Environment.IsDevelopment())
            {
                //use app secrets and local MSSQL 
                dbConnection = builder.Configuration["ConnectionStrings:DefaultConnection"];

                JwtIssuer = builder.Configuration["Jwt:Issuer"];
                JwtAudience = builder.Configuration["Jwt:Audience"];
                JwtKey = builder.Configuration["Jwt:Key"];
            }
            else
            {
                //use Azure Secrets and AzureSql
                var keyVaultUri = builder.Configuration["AzureKeyVault:VaultUri"];
                var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

                KeyVaultSecret KeyVaultJwtIssuer = await client.GetSecretAsync("JwtIssuer");
                KeyVaultSecret KeyVaultJwtAudience = await client.GetSecretAsync("JwtAudience");
                KeyVaultSecret KeyVaultJwtKey = await client.GetSecretAsync("JwtKey");

                KeyVaultSecret secret = await client.GetSecretAsync("DefaultConnection");

                dbConnection = secret.Value;
                JwtAudience = KeyVaultJwtAudience.Value;
                JwtKey = KeyVaultJwtKey.Value;
                JwtIssuer = KeyVaultJwtIssuer.Value;
            }

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = JwtIssuer,
                   ValidAudience = JwtAudience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey))
               };
           });

            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(dbConnection, sqlOptions =>
                         sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

            return builder;
        }
    }
}
