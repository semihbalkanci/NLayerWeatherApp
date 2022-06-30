using Newtonsoft.Json;

namespace NLayer.Core.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WeatherForecastItem
    {
        [JsonProperty("dt"), JsonConverter(typeof(UnixTimeConverter))]
        public DateTimeOffset? CalculationTime { get; internal set; }

        [JsonRequired, JsonProperty("main")]
        public TemperatureInfo? Temperature { get; internal set; }

        [JsonProperty("city")]
        public City City { get; set; }
    }
    public class UnixTimeConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
            => DateTimeOffset.FromUnixTimeSeconds((long)reader.Value);
        public override void WriteJson(JsonWriter writer, DateTimeOffset value, JsonSerializer serializer)
            => writer.WriteValue(value.ToUnixTimeSeconds());
    }
}
