using CoffeeProject.API.Endpoints;
using CoffeeProject.Application.Repositories;
using CoffeeProject.Infrastructure.Repositories;
using Microsoft.OpenApi;

namespace CoffeeProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddScoped<CoffeeMethods>();
            builder.Services.AddTransient<ICoffeeRepository, CoffeeRepository>();
            var openWeatherBaseUrl = builder.Configuration.GetValue<string>("OpenWeatherAPI:BaseUrl") ?? "";

            builder.Services.AddHttpClient<ICoffeeRepository, CoffeeRepository>(client =>
            {
                client.BaseAddress = new Uri(openWeatherBaseUrl);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("coffeeproject", new OpenApiInfo
                {
                    Title = "Coffee Project API",
                    Version = "v1",
                    Description = "Developer Technical Test"
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/coffeeproject/swagger.json", "Coffee Project API");

                c.RoutePrefix = "swagger";
            });

            app.MapCoffeeEndpoints();

            app.UseHttpsRedirection();

            app.Run();

        }
    }
}