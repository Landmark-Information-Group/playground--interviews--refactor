using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Landmark.FloodData.Models
{
	public class EnvironmentAgencyFloodAlert
	{
		[JsonProperty(PropertyName = "@id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "floodAreaId")]
		public string FloodAreaId { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "eaAreaName")]
		public string EaAreaName { get; set; }

		[JsonProperty(PropertyName = "eaRegionName")]
		public string EaRegionName { get; set; }

		[JsonProperty(PropertyName = "isTidal")]
		public string IsTidal { get; set; }

		[JsonProperty(PropertyName = "message")]
		public string Message { get; set; }

		[JsonProperty(PropertyName = "severity")]
		public string Severity { get; set; }

		[JsonProperty(PropertyName = "severityLevel")]
		public int SeverityLevel { get; set; }

		[JsonProperty(PropertyName = "timeRaised")]
		public DateTime TimeRaised { get; set; }

		[JsonProperty(PropertyName = "timeMessageChanged")]
		public DateTime TimeMessageChanged { get; set; }

		[JsonProperty(PropertyName = "timeSeverityChanged")]
		public DateTime TimeSeverityChanged { get; set; }
	}
}