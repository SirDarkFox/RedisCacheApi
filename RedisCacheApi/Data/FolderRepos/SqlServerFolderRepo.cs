using Microsoft.EntityFrameworkCore;
using RedisCacheApi.Models;

namespace RedisCacheApi.Data.FolderRepos
{
    public class SqlServerFolderRepo : ISqlFolderRepo
    {
        private readonly AppDbContext _context;

        public SqlServerFolderRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<DbFolder> GetAllFolders()
        {
            return _context.Folders
                .Include(folder => folder.Folders)
                .Include(file => file.Files)
                .ToList();
        }

        public DbFolder? GetFolderById(string? id)
        {
            return GetAllFolders().FirstOrDefault(f => f.Id == id);
        }

        public void CreateFolder(DbFolder folder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException(nameof(folder));
            }

            _context.Folders.Add(folder);
        }

        public void UpdateFolder(DbFolder folder)
        {
            //No need because of the mapper
        }

        public void DeleteFolder(DbFolder folder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException(nameof(folder));
            }

            _context.Folders.Remove(folder);
        }
    }
}
