using System.ComponentModel.DataAnnotations;

namespace RedisCacheApi.Dtos.FileDtos
{
    public class FileReadDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Content { get; set; }
        public string? FolderId { get; set; }
        public string? FolderName { get; set; }
    }
}
