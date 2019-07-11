using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace SpaceX.LaunchPads.IntegrationTests
{
    public class LaunchPadApi_Should
      : IClassFixture<WebApplicationFactory<SpaceX.LaunchPads.Api.Startup>>
    {
        private readonly WebApplicationFactory<SpaceX.LaunchPads.Api.Startup> factory;

        public LaunchPadApi_Should(WebApplicationFactory<SpaceX.LaunchPads.Api.Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task ReturnId()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("api/launchpads");

            var body = await response.Content.ReadAsStringAsync();

            var launchpads = JsonConvert.DeserializeObject<JArray>(body);

            foreach (var pad in launchpads)
            {
                Assert.NotNull(pad["id"]);
            }
        }

        [Fact]
        public async Task ReturnName()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("api/launchpads");

            var body = await response.Content.ReadAsStringAsync();

            var launchpads = JsonConvert.DeserializeObject<JArray>(body);

            foreach (var pad in launchpads)
            {
                Assert.NotNull(pad["name"]);
            }
        }

        [Fact]
        public async Task ReturnStatus()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("api/launchpads");

            var body = await response.Content.ReadAsStringAsync();

            var launchpads = JsonConvert.DeserializeObject<JArray>(body);

            foreach (var pad in launchpads)
            {
                Assert.NotNull(pad["status"]);
            }
        }
    }
}
