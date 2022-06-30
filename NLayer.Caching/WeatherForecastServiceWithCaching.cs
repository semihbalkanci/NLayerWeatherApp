using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using System.Collections.Specialized;

namespace NLayer.Caching
{
    // open close principle -> open for extension, closed for modification
    public class WeatherForecastServiceWithCaching : IWeatherForecastService
    {
        private const string CacheWeatherForecastKey = "weatherForecastCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IWeatherForecastRepository _repository;

        public WeatherForecastServiceWithCaching(IWeatherForecastRepository repository, IMapper mapper, IMemoryCache memoryCache)
        {
            _repository = repository;
            _mapper = mapper;
            _memoryCache = memoryCache;

            //if (!_memoryCache.TryGetValue(CacheProductKey, out _)) // dont allocate in memory
            //{
            //    _memoryCache.Set(CacheProductKey, _repository.RequestAsync().Result);
            //}

            if (!_memoryCache.TryGetValue(CacheWeatherForecastKey, out _))
            {
                _memoryCache.Set(CacheWeatherForecastKey, _repository.CacheWeatherForecastKey().Result);
            }
        }

        public Task<CustomResponseDto<WeatherForecastOutputDto>> RequestAsync(string apiURL, string appId, WeatherForecastInputDto weatherForecastInputDto)
        {
            throw new NotImplementedException();
        }


        public Task<CustomResponseDto<WeatherForecastOutputDto>> RequestAsyncFiveDays(string apiURL, string appId, NameValueCollection? queryParameters = null)
        {
            throw new NotImplementedException();
        }

        public async Task CacheAllWeatherForecastsAsync()
        {
            //_memoryCache.Set(CacheWeatherForecastKey, await _repository.GetAll().ToListAsync());
            _memoryCache.Set(CacheWeatherForecastKey, await _repository.);
        }
    }
}
