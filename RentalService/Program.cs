using Microsoft.EntityFrameworkCore;
using RentalService.Data;
using RentalService.Repositories;
using RentalService.Services;

namespace RentalService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddAutoMapper(typeof(DataBaseMappings));
            builder.Services.AddScoped<IRentalRepository, RentalRepository>();
            var configuration = builder.Configuration;

            builder.Services.AddDbContext<RentalDbContext>(
                options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString(nameof(RentalDbContext)));
                }
                );
            var app = builder.Build();
            app.MapGrpcService<RentalServiceImpl>();
            // Configure the HTTP request pipeline.
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}