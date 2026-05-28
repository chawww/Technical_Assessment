using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeProject.Application.DTOs
{
    public class CoffeeResponseDto
    {
        public required string Message { get; set; }
        public DateTime Prepared { get; set; }

    }
}
