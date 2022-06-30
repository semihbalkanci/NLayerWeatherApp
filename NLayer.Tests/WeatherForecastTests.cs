using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NLayer.API.Configuration;
using NLayer.API.Controllers;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Service.Services;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Xunit;

namespace NLayer.Tests
{
    public class WeatherForecastTests
    {

        private readonly Mock<ILogger<WeatherForecastController>> _logger;
        private readonly Mock<IWeatherForecastService> _weatherForecastService;
        //private readonly Mock<IOptions<AppSettings>> _appSettings;
        IOptions<AppSettings> _appSettingsOptions;

        public WeatherForecastTests()
        {
            var settings = new AppSettings()
            {
                ApiURL = "https://api.openweathermap.org/data/2.5/forecast",
                AppId = "2cee08870926927091c06991c6cec07d"
            };

            _appSettingsOptions = Options.Create(settings);
            _logger = new Mock<ILogger<WeatherForecastController>>();
            _weatherForecastService = new Mock<IWeatherForecastService>();
            //_appSettings = new Mock<IOptions<AppSettings>>();
        }

        [Fact]
        public async Task Check_WeatherForecastController()
        {
            //Mock<IWeatherForecastRepository> mockRepo = new Mock<IWeatherForecastRepository>();
            //Mock<IWeatherForecastService> mockService = new Mock<IWeatherForecastService>();
            var parameters = new NameValueCollection { { "id", "745042" } };
            //var weatherForecast = await mockService.Object.RequestAsyncFiveDays("https://api.openweathermap.org/data/2.5/forecast", "2cee08870926927091c06991c6cec07d", parameters);
            //appSettings.SetupAllProperties();

            //_appSettings.Object.Value.ApiURL = "https://api.openweathermap.org/data/2.5/forecast";
            //_appSettings.Object.Value.AppId = "2cee08870926927091c06991c6cec07d";

            var controller = new WeatherForecastController(_logger.Object, _weatherForecastService.Object, _appSettingsOptions);

            var weatherForecast = await controller.GetLocationService(745042);

            //"ApiURL": "https://api.openweathermap.org/data/2.5/forecast",
            //"AppId": "2cee08870926927091c06991c6cec07d"
        }
    }
}