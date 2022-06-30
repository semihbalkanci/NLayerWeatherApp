namespace NLayer.Core.DTOs
{
    public class WeatherForecastItemDto
    {
        public DateTimeOffset? CalculationTime { get; internal set; }

        public TemperatureInfoDto? Temperature { get; internal set; }

        public CityDto City { get; internal set; }
    }
}
