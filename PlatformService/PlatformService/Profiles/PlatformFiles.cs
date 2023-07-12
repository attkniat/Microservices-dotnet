using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Model;

namespace PlatformService.Profiles
{
    public class PlatformFiles : Profile
    {
        public PlatformFiles()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publlisher));
        }
    }
}