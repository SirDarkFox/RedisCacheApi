using RedisCacheApi.Models;

namespace RedisCacheApi.Data.FileRepos
{
    public interface ISqlFileRepo
    {
        bool SaveChanges();
        IEnumerable<DbFile>? GetAllFiles();
        DbFile? GetFileById(string? id);
        void CreateFile(DbFile file);
        void UpdateFile(DbFile file);
        void DeleteFile(DbFile file);
        void UpdateLastTimeUsed(DbFile file);
        void UpdateLastTimeUsed(string id);
    }
}
