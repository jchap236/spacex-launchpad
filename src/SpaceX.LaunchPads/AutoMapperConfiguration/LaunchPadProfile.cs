using System;
using AutoMapper;

namespace SpaceX.LaunchPads.AutoMapperConfiguration
{
    public class LaunchPadProfile : Profile
    { 
        public LaunchPadProfile()
        {
            CreateMap<SpaceXLaunchPadResponse, LaunchPad>()
                .ForMember(dest=> dest.Name, opts =>
                {
                    opts.MapFrom(src => src.FullName);
                });
        }
    }
}
