using System.ComponentModel.DataAnnotations;

namespace RedisCacheApi.Models
{
    public class DbFolder
    {
        public string Id { get; set; } = $"Folder:{Guid.NewGuid}";
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public DbFolder? FolderForeign { get; set; }
        public IEnumerable<DbFolder>? Folders { get; set; }
        public IEnumerable<DbFile>? Files { get; set; }
    }
}
