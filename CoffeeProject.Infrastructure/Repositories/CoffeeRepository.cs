using CoffeeProject.Application.DTOs;
using CoffeeProject.Application.Repositories;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CoffeeProject.Infrastructure.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CoffeeRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<CoffeeResponseDto> GetBrewCoffee(string zipCode, string countryCode)
        {
            try
            {
                var apiKey = _configuration["OpenWeatherAPI:ApiKey"] ?? "";

                var location = await _httpClient
                    .GetFromJsonAsync<GeoResponse>(
                       $"geo/1.0/zip?zip={zipCode},{countryCode}&limit=1&appid={apiKey}");

                WeatherResponse? weather = null;
                if (location != null)
                {
                    weather = await _httpClient
                        .GetFromJsonAsync<WeatherResponse>(
                            $"data/2.5/weather?lat={location.Lat}&lon={location.Lon}&units=metric&appid={apiKey}");
                }

                string message = weather?.Main.Temp > 30 
                        ? "Your refreshing iced coffee is ready"
                        : "Your piping hot coffee is ready";

                var response = new CoffeeResponseDto
                {
                    Message = message,
                    Prepared = DateTime.UtcNow
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while brewing coffee.", ex);
            }
        }
    }
}
