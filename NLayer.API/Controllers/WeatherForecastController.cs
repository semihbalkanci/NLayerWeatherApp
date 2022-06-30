using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NLayer.API.Configuration;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using System.Collections.Specialized;

namespace NLayer.API.Controllers
{
    public class WeatherForecastController : CustomBaseController
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly AppSettings _appSettings;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
            _appSettings = appSettings.Value;
        }

        // GET /weather/locations/2345
        [HttpGet]
        [Route("/weather/locations/{id}")]
        public async Task<IActionResult?> GetLocationService(int id)
        {
            _logger.LogInformation("GetLocationService called.");
            var parameters = new NameValueCollection { { "id", id.ToString() } };
            return CreateActionResult(await _weatherForecastService.RequestAsyncFiveDays(_appSettings.ApiURL, _appSettings.AppId, parameters));
        }

        // GET /weather/summary? unit = celsius & temperature = 24 & locations = 2345,1456,7653
        // Using [FromUri] attribute to force Web API to get the value of complex type from the query string
        [HttpGet]
        [Route("/weather/summary/")]
        public async Task<IActionResult?> GetSummaryService([FromQuery] WeatherForecastInputDto weatherForecastInputDto)
        {
            _logger.LogInformation("GetSummaryService called.");
            return CreateActionResult(await _weatherForecastService.RequestAsync(_appSettings.ApiURL, _appSettings.AppId, weatherForecastInputDto));
        }
    }
}