using Newtonsoft.Json;

namespace NLayer.Core.Models
{
    public class WeatherForecast 
    {
        [JsonRequired, JsonProperty("list")]
        public IReadOnlyCollection<WeatherForecastItem> List { get; internal set; } = Array.Empty<WeatherForecastItem>();

        [JsonProperty("city")]
        public City? City { get; set; }
    }
}
