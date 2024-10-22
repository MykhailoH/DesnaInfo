using Newtonsoft.Json;

namespace DesnaInfo.Viber.Classes
{
    public class Rich_Media
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("ButtonsGroupColumns")]
        public int ButtonsGroupColumns { get; set; }
        [JsonProperty("ButtonsGroupRows")]
        public int ButtonsGroupRows { get; set; }
        [JsonProperty("BgColor")]
        public string BgColor { get; set; }
        [JsonProperty("Buttons")]
        public Button[] Buttons { get; set; }
    }
}