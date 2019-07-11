using System;
using Xunit;
using SpaceX.LaunchPads.SpaceXDataService;

namespace SpaceX.LaunchPads.UnitTests
{
    public class LaunchPadFilter_ApplyToUri_Should
    {
        [Theory]
        [InlineData("id,status", "launchpads?filter=id,status")]
        [InlineData("id", "launchpads?filter=id")]
        public void AddFilterParams(string filterProperties, string expected)
        {
            var filter = new LaunchPadFilter();
            filter.Filter = filterProperties;

            var result = filter.ApplyToUri("launchpads");

            Assert.Equal(expected, result);
        }

       [Fact]
        public void AddLimitParams()
        {
            var filter = new LaunchPadFilter();

            filter.Limit = 3;

            var result = filter.ApplyToUri("launchpads");

            Assert.Equal("launchpads?limit=3", result);
        }

        [Fact]
        public void ConvertNameToFullName()
        {
            var filter = new LaunchPadFilter();

            filter.Filter = "id,name,status";

            var result = filter.ApplyToUri("launchpads");

            Assert.Equal("launchpads?filter=id,full_name,status", result);
        }
    }
}
