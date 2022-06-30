using NLayer.Core.Models;
using System.Collections.Specialized;

namespace NLayer.Core.Repositories
{
    public interface IWeatherForecastRepository : IGenericRepository<WeatherForecast>
    {
        public Task<WeatherForecast> RequestAsync(string apiURL, string appId, string unit, NameValueCollection? queryParameters = null);
    }
}
