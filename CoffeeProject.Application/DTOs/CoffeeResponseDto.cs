using Newtonsoft.Json;
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
    public class GeoResponse
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
    public class WeatherResponse
    {
        public MainData Main { get; set; } = new();
    }

    public class MainData
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
    }
}
