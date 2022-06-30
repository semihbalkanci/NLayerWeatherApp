using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System.Collections.Specialized;

namespace NLayer.Repository.Repositories
{
    public class WeatherForecastRepository : GenericRepository<WeatherForecast>, IWeatherForecastRepository
    {
        private readonly IMemoryCache _cache;

        public WeatherForecastRepository(IMemoryCache cache)
        {
            _cache = cache;
        }
        public async Task<WeatherForecast> RequestAsync(string apiURL, string appId, string unit, NameValueCollection? queryParameters = null)
        {
            if (!String.IsNullOrEmpty(unit))
            {
                queryParameters.Add("units", unit == "fahrenheit" ? "imperial" : "metric");
            }

            var cacheKey = BuildRequestCacheKey(queryParameters);
            var cacheItem = _cache.Get<WeatherForecast>(cacheKey);

            if (cacheItem != null)
            {
                return cacheItem;
            }

            var requestUri = BuildRequestUri(apiURL, appId, queryParameters);
            WeatherForecast? result = await SendAsync(HttpMethod.Get, requestUri, null);

            if (result != null)
            {
                _cache.Set(cacheKey, result, DateTimeOffset.UtcNow + TimeSpan.FromDays(1));
            }

            return result;
        }
    }
}
