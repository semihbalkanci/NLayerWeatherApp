using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using System.Collections.Specialized;

namespace NLayer.Service.Services
{
    public class WeatherForecastService : Service<WeatherForecast>, IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        private readonly IMapper _mapper;
        public WeatherForecastService(IGenericRepository<WeatherForecast> repository, IMapper mapper, IWeatherForecastRepository weatherForecastRepository)
        {
            _mapper = mapper;
            _weatherForecastRepository = weatherForecastRepository;
        }

        public async Task<CustomResponseDto<WeatherForecastOutputDto>> RequestAsync(string apiURL, string appId, WeatherForecastInputDto weatherForecastInputDto)
        {
            if (weatherForecastInputDto.Locations == null)
            {
                throw new System.Exception("Input locations are empty.");
            }

            WeatherForecastOutputDto weatherForecastOutputDto = new WeatherForecastOutputDto();
            string[] locations = weatherForecastInputDto.Locations.Trim().Split(",");

            foreach (var location in locations)
            {
                var parameters = new NameValueCollection { { "id", Convert.ToInt32(location).ToString() } };
                var weatherForecast = await _weatherForecastRepository.RequestAsync(apiURL, appId, weatherForecastInputDto.Unit, parameters);

                if (weatherForecast != null)
                {
                    var weatherForecastItem = weatherForecast.List.FirstOrDefault(t => t.Temperature?.Value > weatherForecastInputDto.Temperature && t.CalculationTime.Value.Day == DateTime.Now.AddDays(1).Day && t.CalculationTime.Value.Hour == 12);
                   
                    if (weatherForecastItem != null)
                    {
                        weatherForecastItem.City = weatherForecast.City;
                        var weatherForecastItemDto = _mapper.Map<WeatherForecastItemDto>(weatherForecastItem);
                        weatherForecastOutputDto.WeatherForecastItems.Add(weatherForecastItemDto);
                    }
                }
            }
            return CustomResponseDto<WeatherForecastOutputDto>.Success(200, weatherForecastOutputDto);
        }

        public async Task<CustomResponseDto<WeatherForecastOutputDto>> RequestAsyncFiveDays(string apiURL, string appId, NameValueCollection? queryParameters = null)
        {
            WeatherForecastOutputDto weatherForecastOutputDto = new WeatherForecastOutputDto();

            var weatherForecast = await _weatherForecastRepository.RequestAsync(apiURL, appId, "metric", queryParameters);

            if (weatherForecast != null)
            {
                foreach (WeatherForecastItem weatherForecastItem in weatherForecast.List)
                {
                    if (weatherForecastItem.CalculationTime.Value.Hour == 12)
                    {
                        weatherForecastItem.City = weatherForecast.City;
                        var weatherForecastItemDto = _mapper.Map<WeatherForecastItemDto>(weatherForecastItem);
                        weatherForecastOutputDto.WeatherForecastItems.Add(weatherForecastItemDto);
                    }
                }
            }

            return CustomResponseDto<WeatherForecastOutputDto>.Success(200, weatherForecastOutputDto);
        }

    }
}
