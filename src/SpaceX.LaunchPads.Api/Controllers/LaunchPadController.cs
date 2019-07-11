using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SpaceX.LaunchPads.Api
{
    [Route("api/launchpads")]
    public class LaunchPadController : Controller
    {
        private readonly ILaunchPadRepository launchPadRepository;

        public LaunchPadController(ILaunchPadRepository launchPadRepository)
        {
            this.launchPadRepository = launchPadRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string filter= null, [FromQuery] int? limit = null)
        {
            var launchPadFilter = new LaunchPadFilter()
            {
                Filter = filter,
                Limit = limit
            };

            var launchPads = await this.launchPadRepository.GetAsync(launchPadFilter);

            if (launchPads == null || !launchPads.Any()) return NotFound();

            return Ok(launchPads
                .Select(x => launchPadFilter.FilterProperties(x)));
        }
    }
}
