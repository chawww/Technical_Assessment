using CoffeeProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeProject.Application.Repositories
{
    public interface ICoffeeRepository
    {
        public Task<CoffeeResponseDto> GetBrewCoffee(string zipCode, string countryCode);
    }
}
