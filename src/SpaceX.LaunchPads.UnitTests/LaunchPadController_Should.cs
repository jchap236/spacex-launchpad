using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using SpaceX.LaunchPads.Api;
using Xunit;

namespace SpaceX.LaunchPads.UnitTests
{
    public class LaunchPadController_Should
    {
        private readonly Fixture fixture;

        public LaunchPadController_Should()
        {
            fixture = new Fixture();
        }

        [Fact]
        public async Task Get_ReturnHttpStatus200_WhenSuccessful()
        {
            var launchPads = fixture.CreateMany<LaunchPad>(5);

            var mockRepo = new Mock<ILaunchPadRepository>();

            mockRepo.Setup(repo => repo.GetAsync(It.IsAny<LaunchPadFilter>()))
                .ReturnsAsync(launchPads);

            var controller = new LaunchPadController(mockRepo.Object);

            var result = await controller.Get() as IStatusCodeActionResult;

            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async Task Get_ReturnLaunchPads_WhenSuccessful()
        {
            var launchPads = fixture.CreateMany<LaunchPad>(5);

            var mockRepo = new Mock<ILaunchPadRepository>();

            mockRepo.Setup(repo => repo.GetAsync(It.IsAny<LaunchPadFilter>()))
                .ReturnsAsync(launchPads);

            var controller = new LaunchPadController(mockRepo.Object);

            var result = await controller.Get() as OkObjectResult;

            Assert.Equal(launchPads, result.Value as IEnumerable<dynamic>);
        }

        [Fact]
        public async Task Get_ReturnHttpStatus404_WhenLaunchPadsNull()
        {
            var mockRepo = new Mock<ILaunchPadRepository>();

            mockRepo.Setup(repo => repo.GetAsync(It.IsAny<LaunchPadFilter>()))
                .ReturnsAsync((IEnumerable<LaunchPad>)null);

            var controller = new LaunchPadController(mockRepo.Object);

            var result = await controller.Get() as IStatusCodeActionResult;

            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);

        }

        [Fact]
        public async Task Get_ReturnHttpStatus404_WhenLaunchPadsEmpty()
        {
            var mockRepo = new Mock<ILaunchPadRepository>();

            mockRepo.Setup(repo => repo.GetAsync(It.IsAny<LaunchPadFilter>()))
                .ReturnsAsync((IEnumerable<LaunchPad>)new List<LaunchPad>());

            var controller = new LaunchPadController(mockRepo.Object);

            var result = await controller.Get() as StatusCodeResult;

            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
        }
    }
}
