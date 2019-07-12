using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace SpaceX.LaunchPads.IntegrationTests
{
    public class GetLaunchPads_Filter_Should
      : IClassFixture<WebApplicationFactory<SpaceX.LaunchPads.Api.Startup>>
    {
        private readonly WebApplicationFactory<SpaceX.LaunchPads.Api.Startup> factory;

        public GetLaunchPads_Filter_Should(WebApplicationFactory<SpaceX.LaunchPads.Api.Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task FilterId()
        {
            var client = this.factory.CreateClient();

            var response = await client.GetAsync("api/launchpads?filter=id");

            var body = await response.Content.ReadAsStringAsync();

            var launchpads = JsonConvert.DeserializeObject<JArray>(body);

            foreach (var pad in launchpads)
            {
                Assert.NotNull(pad["id"]);
                Assert.Single(pad);
            }
        }

        [Fact]
        public async Task FilterName()
        {
            var client = this.factory.CreateClient();

            var response = await client.GetAsync("api/launchpads?filter=name");

            var body = await response.Content.ReadAsStringAsync();

            var launchpads = JsonConvert.DeserializeObject<JArray>(body);

            foreach (var pad in launchpads)
            {
                Assert.NotNull(pad["name"]);
                Assert.Single(pad);
            }
        }

        [Fact]
        public async Task FilterStatus()
        {
            var client = this.factory.CreateClient();

            var response = await client.GetAsync("api/launchpads?filter=status");

            var body = await response.Content.ReadAsStringAsync();

            var launchpads = JsonConvert.DeserializeObject<JArray>(body);

            foreach (var pad in launchpads)
            {
                Assert.NotNull(pad["status"]);
                Assert.Single(pad);
            }
        }

        [Fact]
        public async Task LimitResultSet()
        {
            var client = this.factory.CreateClient();

            var response = await client.GetAsync("api/launchpads?limit=2");

            var body = await response.Content.ReadAsStringAsync();

            var launchpads = JsonConvert.DeserializeObject<JArray>(body);

            Assert.Equal(2, (int)launchpads.Count());
        }
    }
}
