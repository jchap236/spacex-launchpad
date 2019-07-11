using System;
using Newtonsoft.Json;

namespace SpaceX.LaunchPads
{
    public class SpaceXLaunchPadResponse
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
