using Newtonsoft.Json;

namespace DesnaInfo.Viber.Classes
{
    public class Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        public string Media { get; set; }
        public Location Location { get; set; }
        [JsonProperty("tracking_data")]
        public string Tracking_Data { get; set; }
    }

    public class WelcomeMessage : Message
    {
        public Sender Sender { get; set; }
    }

    public class RegularMessage : Message
    {
        [JsonProperty("sender")]
        public Sender Sender { get; set; }
        [JsonProperty("receiver")]
        public string Receiver { get; set; }
        [JsonProperty("min_api_version")]
        public int MinApiVersion { get; set; }
    }
}