using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SpaceX.LaunchPads.SpaceXDataService
{
    public class LaunchPadServiceRepository : ILaunchPadRepository
    {
        private readonly HttpClient client;
        private readonly IMapper mapper;
        private readonly ILogger<LaunchPadServiceRepository> log;

        public LaunchPadServiceRepository(HttpClient client, IMapper mapper, ILogger<LaunchPadServiceRepository> log)
        {
            this.client = client;
            this.mapper = mapper;
            this.log = log;
        }

        public async Task<IEnumerable<LaunchPad>> GetAsync(LaunchPadFilter filter = null)
        {
            var requestUri = "launchpads";

            if (filter != null) requestUri = filter.ApplyToUri(requestUri);

            var response = await this.client.GetAsync(requestUri);

            if(response.StatusCode != HttpStatusCode.OK) log.LogWarning($"Unexpected Response from SpaceX API:{response.StatusCode} at {DateTime.UtcNow.ToString()}");

            if (response.StatusCode == HttpStatusCode.NotFound) return new List<LaunchPad>();

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var spaceXData = JsonConvert.DeserializeObject<IEnumerable<SpaceXLaunchPadResponse>>(body);

            return spaceXData.Select(x => mapper.Map<LaunchPad>(x));
        }
    }
}
