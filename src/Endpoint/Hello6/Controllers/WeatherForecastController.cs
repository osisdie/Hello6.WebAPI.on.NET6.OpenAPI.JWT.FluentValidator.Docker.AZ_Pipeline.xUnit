using System;
using System.Collections.Generic;
using System.Linq;
using Hello6.Domain.Contract.Models.WeatherForecast;
using Hello6.Domain.Endpoint.Controllers.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hello6.Domain.Endpoint.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : DomainContollerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast_ResponseDto> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast_ResponseDto
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
