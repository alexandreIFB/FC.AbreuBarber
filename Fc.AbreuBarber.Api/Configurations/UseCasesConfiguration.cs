using FC.AbreuBarber.Application.Interfaces;
using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.Domain.Repository;
using FC.AbreuBarber.Infra.Data.EF;
using FC.AbreuBarber.Infra.Data.EF.Repositories;
using System.Net.NetworkInformation;

namespace Fc.AbreuBarber.Api.Configurations
{
    public static class UseCasesConfiguration
    {
        public static IServiceCollection AddUseCases(
        this IServiceCollection services)
        {

            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(typeof(CreateProcedure).Assembly)
            );

            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(
        this IServiceCollection services)
        {

            services.AddTransient<IProcedureRepository, ProcedureRepository>();
            services.AddTransient<IUnityOfWork, UnitOfWork>();

            return services;
        }
    }
}
