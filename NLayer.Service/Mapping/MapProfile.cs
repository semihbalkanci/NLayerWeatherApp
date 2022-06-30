using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<WeatherForecastItem, WeatherForecastItemDto>().ReverseMap();
            CreateMap<TemperatureInfo, TemperatureInfoDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
