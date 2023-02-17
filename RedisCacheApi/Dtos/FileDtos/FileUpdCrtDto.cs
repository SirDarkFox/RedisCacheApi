using System.ComponentModel.DataAnnotations;

namespace RedisCacheApi.Dtos.FileDtos
{
    public class FileUpdCrtDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Content { get; set; }
        public string? FolderId { get; set; }
    }
}
