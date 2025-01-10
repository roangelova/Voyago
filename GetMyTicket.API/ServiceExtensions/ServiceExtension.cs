using GetMyTicket.Persistance.Generic_Repository;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;
using GetMyTicket.Service.Services;

namespace GetMyTicket.API.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITransportationProviderService, TransportationProviderService>();

            return services;
        }
    }
}
