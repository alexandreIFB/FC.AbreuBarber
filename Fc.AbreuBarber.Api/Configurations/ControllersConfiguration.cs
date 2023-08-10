using Fc.AbreuBarber.Api.Filters;
using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;

namespace Fc.AbreuBarber.Api.Configurations
{
    public static class ControllersConfiguration
    {

        public static IServiceCollection AddAndConfigureControllers
            (this IServiceCollection services)
        {
            services.AddControllers(options 
                => options.Filters.Add(typeof(ApiGlobalExpectionFilter)));
            services.AddDocumentation();

            return services;
        }

        private static IServiceCollection AddDocumentation
            (this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static WebApplication UseDocumentation(
            this WebApplication app
            )
        {
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }

    }
}
