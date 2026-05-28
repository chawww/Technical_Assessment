using CoffeeProject.Application.DTOs;
using CoffeeProject.Application.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace CoffeeProject.Infrastructure.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        public Task<CoffeeResponseDto> GetBrewCoffee()
        {
            try
            {
                var response = new CoffeeResponseDto
                {
                    Message = "Your piping hot coffee is ready",
                    Prepared = DateTime.UtcNow
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while brewing coffee.", ex);
            }
        }
    }
}
