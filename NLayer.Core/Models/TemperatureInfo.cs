using Newtonsoft.Json;

namespace NLayer.Core.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class TemperatureInfo
    {
        //[JsonRequired, JsonProperty("feels_like")]
        //public double FeelsLike { get; internal set; }

        //[JsonProperty("temp_max")]
        //public double? MaximumTemperature { get; internal set; }

        //[JsonProperty("temp_min")]
        //public double? MinimumTemperature { get; internal set; }

        //[JsonProperty("sea_level")]
        //public double? SeaLevel { get; internal set; }

        [JsonRequired, JsonProperty("temp")]
        public double Value { get; internal set; }
    }
}
