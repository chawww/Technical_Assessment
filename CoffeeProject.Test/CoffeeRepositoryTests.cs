using System.Net;
using System.Net.Http.Json;
using CoffeeProject.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Xunit;

namespace CoffeeProject.Test.Infrastructure;

public class CoffeeRepositoryTests
{
    [Fact]
    public async Task GetBrewCoffee_ShouldReturnIcedCoffee_WhenTemperatureAbove30()
    {
        var handler = new Mock<HttpMessageHandler>();

        handler.Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())

            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new
                {
                    lat = 14.5995,
                    lon = 120.9842
                })
            })

            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new
                {
                    main = new
                    {
                        temp = 35
                    }
                })
            });

        var httpClient = new HttpClient(handler.Object)
        {
            BaseAddress = new Uri("https://api.openweathermap.org/")
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["OpenWeatherAPI:ApiKey"] = "fake-api-key"
            })
            .Build();

        var repository = new CoffeeRepository(httpClient, configuration);

        var result = await repository.GetBrewCoffee("1000", "PH");

        Assert.Equal(
            "Your refreshing iced coffee is ready",
            result.Message);
    }

    [Fact]
    public async Task GetBrewCoffee_ShouldReturnHotCoffee_WhenTemperatureBelow30()
    {
        var handler = new Mock<HttpMessageHandler>();

        handler.Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())

            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new
                {
                    lat = 14.5995,
                    lon = 120.9842
                })
            })

            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new
                {
                    main = new
                    {
                        temp = 20
                    }
                })
            });

        var httpClient = new HttpClient(handler.Object)
        {
            BaseAddress = new Uri("https://api.openweathermap.org/")
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["OpenWeatherAPI:ApiKey"] = "fake-api-key"
            })
            .Build();

        var repository = new CoffeeRepository(httpClient, configuration);

        var result = await repository.GetBrewCoffee("1000", "PH");

        Assert.Equal(
            "Your piping hot coffee is ready",
            result.Message);
    }
}