using System;
using AutoMapper;
using Xunit;
using SpaceX.LaunchPads.AutoMapperConfiguration;
using AutoFixture;

namespace SpaceX.LaunchPads.UnitTests
{
    public class LaunchPadProfile_Should
    {
        private readonly SpaceXLaunchPadResponse spaceXResponse;
        private readonly LaunchPad launchPad;

        public LaunchPadProfile_Should()
        {
            var fixture = new Fixture();
            spaceXResponse = fixture.Create<SpaceXLaunchPadResponse>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<LaunchPadProfile>());
            var mapper = config.CreateMapper();

            launchPad = mapper.Map<LaunchPad>(spaceXResponse);
        }

        [Fact]
        public void MapName()
        {
            Assert.Equal(spaceXResponse.FullName, launchPad.Name);
        }

        [Fact]
        public void MapId()
        {
            Assert.Equal(spaceXResponse.Id, launchPad.Id);
        }

        [Fact]
        public void MapStatus()
        {
            Assert.Equal(spaceXResponse.Status, launchPad.Status);
        }
    }
}
