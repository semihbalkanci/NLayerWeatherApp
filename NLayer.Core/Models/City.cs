using Newtonsoft.Json;

namespace NLayer.Core.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class City
    {
        [JsonProperty("id")]
        public string Id { get; internal set; } = null!;

        [JsonProperty("name")]
        public string Name { get; internal set; } = null!;

        [JsonProperty("country")]
        public string Country { get; internal set; } = null!;
    }
}
