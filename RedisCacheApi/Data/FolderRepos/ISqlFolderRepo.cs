namespace RedisCacheApi.Data.FolderRepos
{
    public interface ISqlFolderRepo
    {
        bool SaveChanges();
        IEnumerable<DbFolder> GetAllFolders();
        DbFolder? GetFolderById(string? id);
        void CreateFolder(DbFolder folder);
        void UpdateFolder(DbFolder folder);
        void DeleteFolder(DbFolder folder);
    }
}
