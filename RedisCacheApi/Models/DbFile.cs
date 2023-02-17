using System.ComponentModel.DataAnnotations;

namespace RedisCacheApi.Models
{
    public class DbFile
    {
        public string Id { get; set; } = $"File:{Guid.NewGuid}";
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
        public string? Content { get; set; }
        public DateTime LastTimeUsed { get; set; } = default;
        public virtual DbFolder? FolderForeign { get; set; }
    }
}
