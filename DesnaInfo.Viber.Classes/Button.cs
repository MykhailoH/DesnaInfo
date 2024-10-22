using Newtonsoft.Json;

namespace DesnaInfo.Viber.Classes
{
    public class Button
    {
        [JsonProperty("Columns")]
        public int Columns { get; set; }
        [JsonProperty("Rows")]
        public int Rows { get; set; }
        [JsonProperty("ActionType")]
        public string ActionType { get; set; }
        [JsonProperty("ActionBody")]
        public string ActionBody { get; set; }
        [JsonProperty("Image")]
        public string Image { get; set; }
        [JsonProperty("Text")]
        public string Text { get; set; }
        [JsonProperty("TextSize")]
        public string TextSize { get; set; }
        [JsonProperty("TextVAlign")]
        public string TextVAlign { get; set; }
        [JsonProperty("TextHAlign")]
        public string TextHAlign { get; set; }
    }
}