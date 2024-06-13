using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using APIGateway.Models;
namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOcelot();
            builder.Services.AddSwaggerForOcelot(builder.Configuration);

            // Register User class with dependency injection container
            builder.Services.AddScoped<User>();
            builder.Services.AddScoped<Scooter>();

            builder.Configuration.AddJsonFile("ocelot.json");

            WebApplication app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseSwaggerForOcelotUI(option =>
            {
                option.PathToSwaggerGenerator = "/swagger";
            });
            app.MapControllers();

            app.UseOcelot();
            app.RunAsync();
        }

    }
}
