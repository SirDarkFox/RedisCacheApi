using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RedisCacheApi.Models
{
    public class DbFolder
    {
        public string Id { get; set; } = $"Folder:{Guid.NewGuid}";
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public virtual DbFolder? FolderForeign { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<DbFolder>? Folders { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<DbFile>? Files { get; set; }
    }
}
