using CoffeeProject.API.Endpoints;
using CoffeeProject.Application.DTOs;
using CoffeeProject.Application.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Xunit;

namespace CoffeeProject.Test.API;

public class CoffeeMethodsTests
{
    [Fact]
    public async Task GetBrewCoffee_ShouldReturnOk_WhenRepositoryReturnsData()
    {
        var mockRepo = new Mock<ICoffeeRepository>();

        mockRepo.Setup(x => x.GetBrewCoffee())
            .ReturnsAsync(new CoffeeResponseDto
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTime.UtcNow
            });

        var methods = new CoffeeMethods(mockRepo.Object);

        var result = await methods.GetBrewCoffee();

        Assert.IsType<Ok<CoffeeResponseDto>>(result);
    }

    [Fact]
    public async Task GetBrewCoffee_ShouldReturnNotFound_WhenRepositoryReturnsNull()
    {
        var mockRepo = new Mock<ICoffeeRepository>();

        mockRepo.Setup(x => x.GetBrewCoffee())
            .ReturnsAsync((CoffeeResponseDto?)null);

        var methods = new CoffeeMethods(mockRepo.Object);

        var result = await methods.GetBrewCoffee();

        Assert.IsType<NotFound>(result);
    }
}