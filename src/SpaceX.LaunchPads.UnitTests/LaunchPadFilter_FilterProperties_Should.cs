using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Xunit;

namespace SpaceX.LaunchPads.UnitTests
{
    public class LaunchPadFilter_FilterProperties_Should
    {
        private readonly LaunchPad launchPad;

        public LaunchPadFilter_FilterProperties_Should()
        {
            var fixture = new Fixture();
            launchPad = fixture.Create<LaunchPad>();
        }

        [Fact]
        public void OnlyReturnPropertiesInFilter()
        {
            var launchPadFilter = new LaunchPadFilter()
            {
                Filter = "id,status"
            };

            var filteredObject = launchPadFilter.FilterProperties(launchPad);
            var properties = filteredObject as IDictionary<string, Object>;
            Assert.Equal(2, (int)properties.Count());

        }

        [Fact]
        public void IgnorePropertiesInFilter_WhenPropertyDoesNotExist()
        {
            var launchPadFilter = new LaunchPadFilter()
            {
                Filter = "id,status,foo"
            };

            var filteredObject = launchPadFilter.FilterProperties(launchPad);
            var properties = filteredObject as IDictionary<string, Object>;
            Assert.Equal(2, (int)properties.Count());
        }
    }
}
