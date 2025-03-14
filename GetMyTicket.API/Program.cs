using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using GetMyTicket.Persistance.Context;
using GetMyTicket.API.ExceptionHandler;
using GetMyTicket.API.ServiceExtensions;
using GetMyTicket.Common.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var keyVaultUri = builder.Configuration["AzureKeyVault:VaultUri"];
var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add services to the container.
builder.Services.AddIdentity<User, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

KeyVaultSecret JwtIssuer = await client.GetSecretAsync("JwtIssuer");
KeyVaultSecret JwtAudience = await client.GetSecretAsync("JwtAudience");
KeyVaultSecret JwtKey = await client.GetSecretAsync("JwtKey");

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
            ValidIssuer = JwtIssuer.Value,
            ValidAudience = JwtAudience.Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey.Value))
        };
    });

builder.Services.AddAuthorization();

KeyVaultSecret secret = await client.GetSecretAsync("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(secret.Value, sqlOptions =>
         sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

builder.Services.AddApplicationServices();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("ReactFrontend");

app.UseExceptionHandler();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = string.Empty;
    });
}

using (var scope = app.Services.CreateScope())
{
    try
    {
        var DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        DbContext.Database.Migrate();
        var strategy = DbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await DbContext.Database.BeginTransactionAsync();
            try
            {
                await AppDbContext.SeedDataAsync(DbContext);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }
    catch (Exception)
    {
        throw;
    }
}

app.UseAuthentication();

app.Use(async (context, next) =>
{
    Console.WriteLine($"User: {context?.User.Identity?.Name} - Authenticated: {context?.User.Identity?.IsAuthenticated}");
    await next();
});

app.UseAuthorization();

app.MapControllers();

app.MapIdentityApi<User>();

app.Run();
