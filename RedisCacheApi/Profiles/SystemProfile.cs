using AutoMapper;
using RedisCacheApi.Dtos.SystemDtos;
using RedisCacheApi.Models;

namespace RedisCacheApi.Profiles
{
    public class SystemProfile : Profile
    {
        public SystemProfile()
        {
            CreateMap<DbFolder, SystemObjDto>()
                .ForMember(dest => dest.Folders, opt => opt.MapFrom(src => (src.Folders == null) ? null : src.Folders))
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => (src.Files == null) ? null : src.Files));
            CreateMap<SystemObjDto, DbFolder>()
                .ForMember(dest => dest.Folders, opt => opt.MapFrom(src => (src.Folders == null) ? null : src.Folders))
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => (src.Files == null) ? null : src.Files));

            CreateMap<DbFile, SystemObjDto>();
            CreateMap<SystemObjDto, DbFile>();

            CreateMap<DbFolder, ShowcaseDto>();
            CreateMap<ShowcaseDto, DbFolder>();

            CreateMap<DbFile, ShowcaseDto>();
            CreateMap<ShowcaseDto, DbFile>();
        }
    }
}
