using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CourseraDotNet.Core.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NavigationOption
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
