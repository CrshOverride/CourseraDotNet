using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CourseraDotNet.Core.Models
{
    public class CourseDetail
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "start_day")]
        public int? StartDay { get; set; }

        [JsonProperty(PropertyName = "start_month")]
        public int? StartMonth { get; set; }

        [JsonProperty(PropertyName = "start_year")]
        public int? StartYear { get; set; }

        [JsonProperty(PropertyName = "status")]
        public bool IsNotCompleted { get; set; }

        [JsonProperty(PropertyName = "home_link")]
        public string HomeLink { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public bool IsSelfPaced
        {
            get
            {
                return StartDay == null && StartMonth == null && StartYear == null;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return new DateTime(StartYear ?? 1, StartMonth ?? 1, StartDay ?? 1);
            }
        }

        public CourseStatus Status
        {
            get
            {
                if (!IsNotCompleted) return CourseStatus.Completed;
                if (!IsSelfPaced && StartDate > DateTime.Now) return CourseStatus.Pending;
                return CourseStatus.Active;
            }
        }
    }

    public enum CourseStatus
    {
        Completed,
        Active,
        Pending
    }
}
