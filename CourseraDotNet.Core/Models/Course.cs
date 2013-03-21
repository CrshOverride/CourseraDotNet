using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CourseraDotNet.Core.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Course
    {
        [JsonProperty(PropertyName = "categories")]
        public List<Category> Categories { get; set; }

        [JsonProperty(PropertyName = "courses")]
        public List<CourseDetail> CourseDetails { get; set; }

        [JsonProperty(PropertyName = "universities")]
        public List<University> Universities { get; set; }

        [JsonProperty(PropertyName = "display")]
        public bool IsDisplayed { get; set; }

        [JsonProperty(PropertyName = "instructor")]
        public string Instructor { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty(PropertyName = "small_icon")]
        public string SmallIconUrl { get; set; }
    }
}
