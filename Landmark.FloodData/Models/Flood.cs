using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Landmark.FloodData.Models
{
    public class Flood
    {
        public string Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel Severity { get; set; }

        public string Region { get; set; }

        public string FloodAreaId { get; set; }

        public string EaAreaName { get; set; }

        public DateTime TimeRaised { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FloodAction Action { get; set; }
    }
}