﻿using GetMyTicket.Persistance.Generic_Repository;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service;
using GetMyTicket.Service.Contracts;
using GetMyTicket.Service.Services;
using Microsoft.OpenApi.Models;

namespace GetMyTicket.API.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //ADD SWAGGER 
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Get my ticket",
                    Version = "v1",
                    Description = "An example API for .NET 9"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });

            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<TripValidator>();

            services.AddScoped<ITransportationProviderService, TransportationProviderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAirplaneService, AirplaneService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ITripService, TripService>();
            services.AddScoped<ITrainService, TrainService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPassengerService, PassengerService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IBaggageItemService, BaggageItemService>();
            services.AddScoped<IBaggagePriceService, BaggagePriceService>();

            return services;
        }
    }
}
