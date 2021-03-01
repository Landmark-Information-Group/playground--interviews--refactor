using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Landmark.FloodData.Models;
using Newtonsoft.Json;
using WebGrease;

namespace Landmark.FloodData.Controllers
{
	public class FloodController : ApiController
	{
		public async Task<IHttpActionResult> Get()
		{
			try
			{
				var environmentAgencyApiResponse = await GetEnvironmentAgencyData();

				if (environmentAgencyApiResponse.StatusCode != HttpStatusCode.OK)
				{
					return NotFound();
				}

				var environmentAgencyApiResponseContent =
					await environmentAgencyApiResponse.Content.ReadAsStringAsync();

				var environmentAgencyFloodAlerts =
					JsonConvert.DeserializeObject<EnvironmentAgencyFloodAlertServicePayload>(
						environmentAgencyApiResponseContent);

				var processedData = ProcessDataData(environmentAgencyFloodAlerts);

				return Ok(processedData);
			}
			catch (Exception e)
			{
				return InternalServerError();
			}
		}

		public async Task<IHttpActionResult> Get(string eaAreaName)
		{
			try
			{
				var environmentAgencyApiResponse = await GetEnvironmentAgencyData();

				if (environmentAgencyApiResponse.StatusCode != HttpStatusCode.OK)
				{
					return NotFound();
				}

				var environmentAgencyApiResponseContent =
					await environmentAgencyApiResponse.Content.ReadAsStringAsync();

				var environmentAgencyFloodAlerts =
					JsonConvert.DeserializeObject<EnvironmentAgencyFloodAlertServicePayload>(
						environmentAgencyApiResponseContent);

				var processedData = ProcessDataData(environmentAgencyFloodAlerts);

				var filteredData = FilterData(processedData, eaAreaName);

				return Ok(filteredData);
			}
			catch (Exception e)
			{
				return InternalServerError();
			}
		}

		private async Task<dynamic> GetEnvironmentAgencyData()
		{
			using (var client = new HttpClient {BaseAddress = new Uri("http://environment.data.gov.uk")})
			{
				return await client.GetAsync("flood-monitoring/id/floods");
			}
		}

		private IEnumerable<Flood> ProcessDataData(EnvironmentAgencyFloodAlertServicePayload environmentAgencyFloodAlerts)
		{
			var floodData = new List<Flood>();

			if (environmentAgencyFloodAlerts == null || !environmentAgencyFloodAlerts.Items.Any())
			{
				return floodData;
			}

			foreach (var item in environmentAgencyFloodAlerts.Items)
			{
				var itemId = item.Id.Replace("http://environment.data.gov.uk/flood-monitoring/id/floods/", "");

				var flood = new Flood
				{
					Id = itemId,
					Region = item.EaRegionName,
					FloodAreaId = item.FloodAreaId,
					EaAreaName = item.EaAreaName,
					TimeRaised = item.TimeRaised,
					Severity = (SeverityLevel) item.SeverityLevel
				};

				switch (item.EaAreaName.ToLower())
				{
					case "yorkshire":
					case "west midlands":
					{
						flood.Action = FloodAction.MonitorHourly;
						break;
					}
					case "east anglia":
					{
						flood.Action = FloodAction.MonitorDaily;
						break;
					}
					default:
					{
						flood.Action = FloodAction.Ignore;
						break;
					}
				}

				floodData.Add(flood);
			}

			return floodData;
		}

		private IEnumerable<Flood> FilterData(IEnumerable<Flood> inputFloodData, string eaAreaName)
		{
			var floodData = new List<Flood>();

			foreach (var item in inputFloodData)
			{
				if (!string.Equals(item.EaAreaName, eaAreaName, StringComparison.CurrentCultureIgnoreCase))
				{
					continue;
				}

				floodData.Add(item);
			}

			return floodData;
		}
	}
}
