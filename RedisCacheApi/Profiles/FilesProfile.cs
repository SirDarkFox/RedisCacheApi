using AutoMapper;
using RedisCacheApi.Dtos.FileDtos;
using RedisCacheApi.Models;

namespace RedisCacheApi.Profiles
{
    public class FilesProfile : Profile
    {
        public FilesProfile()
        {
            CreateMap<DbFile, FileReadDto>()
                .ForMember(dest => dest.FolderId, opt => opt.MapFrom(src => (src.FolderForeign == null) ? null : src.FolderForeign.Id))
                .ForMember(dest => dest.FolderName, opt => opt.MapFrom(src => (src.FolderForeign == null) ? null : src.FolderForeign.Name));
            CreateMap<FileReadDto, DbFile>();

            CreateMap<DbFile, FileCreateDto>()
                .ForMember(dest => dest.FolderId, opt => opt.MapFrom(src => (src.FolderForeign == null) ? null : src.FolderForeign.Id));
            CreateMap<FileCreateDto, DbFile>();

            CreateMap<DbFile, FileUpdateDto>()
                .ForMember(dest => dest.FolderId, opt => opt.MapFrom(src => (src.FolderForeign == null) ? null : src.FolderForeign.Id));
            CreateMap<FileUpdateDto, DbFile>();
        }
    }
}
