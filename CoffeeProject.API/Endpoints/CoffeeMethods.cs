using CoffeeProject.Application.Repositories;

namespace CoffeeProject.API.Endpoints
{
    public class CoffeeMethods
    {
        private readonly ICoffeeRepository repo;

        public CoffeeMethods(ICoffeeRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IResult> GetBrewCoffee()
        {
            var result = await repo.GetBrewCoffee();
            return result == null ? TypedResults.NotFound() : TypedResults.Ok(result);
        }
    }
}
