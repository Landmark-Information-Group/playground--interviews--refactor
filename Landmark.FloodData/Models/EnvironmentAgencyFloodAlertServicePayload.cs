using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Landmark.FloodData.Models
{
	public class EnvironmentAgencyFloodAlertServicePayload
	{
		[JsonProperty(PropertyName = "items")]
		public IEnumerable<EnvironmentAgencyFloodAlert> Items { get; set; }
	}
}
