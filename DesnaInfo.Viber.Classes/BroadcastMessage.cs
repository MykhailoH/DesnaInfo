using Newtonsoft.Json;

namespace DesnaInfo.Viber.Classes
{
    public class BroadcastMessage 
    {
        [JsonProperty("sender")]
        public Sender Sender { get; set; }
        [JsonProperty("min_api_version")]
        public int Min_Api_Version { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("broadcast_list")]
        public string[] Broadcast_List { get; set; }
        [JsonProperty("rich_media")]
        public Rich_Media Rich_Media { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}