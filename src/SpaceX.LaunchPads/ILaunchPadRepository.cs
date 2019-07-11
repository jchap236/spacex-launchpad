using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceX.LaunchPads
{
    public interface ILaunchPadRepository
    {
        Task<IEnumerable<LaunchPad>> GetAsync(LaunchPadFilter filter = null);
    }
}