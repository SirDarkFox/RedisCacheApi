using AutoMapper;
using RedisCacheApi.Dtos.FolderDtos;
using RedisCacheApi.Models;

namespace RedisCacheApi.Profiles
{
    public class FoldersProfile : Profile
    {
        public FoldersProfile()
        {
            CreateMap<DbFolder, FolderReadDto>()
                .ForMember(dest => dest.FolderId, opt => opt.MapFrom(src => (src.FolderForeign == null) ? null : src.FolderForeign.Id))
                .ForMember(dest => dest.FolderName, opt => opt.MapFrom(src => (src.FolderForeign == null) ? null : src.FolderForeign.Name));
            CreateMap<FolderReadDto, DbFolder>();

            CreateMap<DbFolder, FolderCreateDto>()
                .ForMember(dest => dest.FolderId, opt => opt.MapFrom(src => (src.FolderForeign == null) ? null : src.FolderForeign.Id));
            CreateMap<FolderCreateDto, DbFolder>();

            CreateMap<DbFolder, FolderUpdateDto>()
                .ForMember(dest => dest.FolderId, opt => opt.MapFrom(src => (src.FolderForeign == null) ? null : src.FolderForeign.Id));
            CreateMap<FolderUpdateDto, DbFolder>();
        }
    }
}
