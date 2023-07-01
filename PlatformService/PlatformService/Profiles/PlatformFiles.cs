using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Model;

namespace PlatformService.Profiles
{
    public class PlatformFiles: Profile
    {
        public PlatformFiles()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}