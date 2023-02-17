using RedisCacheApi.Dtos.SystemDtos;

namespace RedisCacheApi.Dtos.FolderDtos
{
    public class FolderOpenDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public IEnumerable<ShowcaseDto>? Folders { get; set; }
        public IEnumerable<ShowcaseDto>? Files { get; set; }
    }
}
