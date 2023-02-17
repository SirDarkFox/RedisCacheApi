using System.ComponentModel.DataAnnotations;

namespace RedisCacheApi.Dtos.FolderDtos
{
    public class FolderUpdCrtDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? FolderId { get; set; }
    }
}
