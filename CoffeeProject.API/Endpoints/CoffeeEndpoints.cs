namespace CoffeeProject.API.Endpoints
{
    public static class CoffeeEndpoints
    {
        private static int apiRequestCounter = 0;
        public static IEndpointRouteBuilder MapCoffeeEndpoints(this IEndpointRouteBuilder builder)
        {
            _ = builder.MapGet("/brew-coffee", async (CoffeeMethods methods, string zipCode, string countryCode) =>
                {
                    if (DateTime.Today.Month == 4 && DateTime.Today.Day == 1)
                    {
                        return Results.StatusCode(StatusCodes.Status418ImATeapot);
                    }

                    apiRequestCounter++;
                    if (apiRequestCounter % 5 == 0)
                    {
                        return Results.StatusCode(StatusCodes.Status503ServiceUnavailable);
                    }

                    return await methods.GetBrewCoffee(zipCode, countryCode);
                });

            return builder;
        }
    }
}
