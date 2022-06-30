namespace NLayer.Core.DTOs
{
    public class WeatherForecastOutputDto
    {
        public List<WeatherForecastItemDto> WeatherForecastItems { get; set; }
        public WeatherForecastOutputDto()
        {
            this.WeatherForecastItems = new List<WeatherForecastItemDto>();
        }
    }
}
