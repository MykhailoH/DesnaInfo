using Newtonsoft.Json;

namespace DesnaInfo.Viber.Classes
{
    public class Sender
    {
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public int Api_Version { get; set; }
    }
}