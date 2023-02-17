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

            CreateMap<DbFolder, FolderOpenDto>()
                .ForMember(dest => dest.Folders, opt => opt.MapFrom(src => (src.Folders == null) ? null : src.Folders))
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => (src.Files == null) ? null : src.Files));
            CreateMap<FolderOpenDto, DbFolder>()
                .ForMember(dest => dest.Folders, opt => opt.MapFrom(src => (src.Folders == null) ? null : src.Folders))
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => (src.Files == null) ? null : src.Files));
        }
    }
}
