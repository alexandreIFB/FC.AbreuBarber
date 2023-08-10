using Fc.AbreuBarber.Api.Configurations;

namespace Fc.AbreuBarber.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                .AddCustomConnections()
                .AddUseCases()
                .AddAndConfigureControllers();

            var app = builder.Build();

            app.UseDocumentation();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}