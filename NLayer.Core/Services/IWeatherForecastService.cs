using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System.Collections.Specialized;

namespace NLayer.Core.Services
{
    public interface IWeatherForecastService : IService<WeatherForecast>
    {
        public Task<CustomResponseDto<WeatherForecastOutputDto>> RequestAsyncFiveDays(string apiURL, string appId, NameValueCollection? queryParameters = null);
        public Task<CustomResponseDto<WeatherForecastOutputDto>> RequestAsync(string apiURL, string appId, WeatherForecastInputDto weatherForecastInputDto);
    }
}
