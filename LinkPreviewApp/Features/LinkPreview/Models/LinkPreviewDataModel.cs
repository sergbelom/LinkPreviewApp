using Newtonsoft.Json;

namespace LinkPreviewApp.Features.LinkPreview.Models
{
    public class LinkPreviewDataModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("imageX")]
        public int ImageX { get; set; }

        [JsonProperty("iconType")]
        public string IconType { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
