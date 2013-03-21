using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CourseraDotNet.Core.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Lecture
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "video_url")]
        public string ProtectedVideoUrl { get; set; }

        [JsonProperty(PropertyName = "lecture_url")]
        public string PdfUrl { get; set; }

        [JsonProperty(PropertyName = "iframe_url")]
        public string IframeUrl { get; set; }

        [JsonProperty(PropertyName = "is_viewed")]
        public bool IsViewed { get; set; }
    }
}
