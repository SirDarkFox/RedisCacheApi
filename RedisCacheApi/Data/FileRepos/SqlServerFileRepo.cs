using RedisCacheApi.Models;

namespace RedisCacheApi.Data.FileRepos
{
    public class SqlServerFileRepo : ISqlFileRepo
    {
        private readonly AppDbContext _context;

        public SqlServerFileRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<DbFile> GetAllFiles()
        {
            return _context.Files.ToList();
        }

        public DbFile? GetFileById(string? id)
        {
            return _context.Files.FirstOrDefault(f => f.Id == id);
        }

        public void CreateFile(DbFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            _context.Files.Add(file);
        }

        public void UpdateFile(DbFile file)
        {
            //No need because of the mapper
        }

        public void DeleteFile(DbFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            _context.Files.Remove(file);
        }

        public void UpdateLastTimeUsed(DbFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            file.LastTimeUsed = DateTime.Now;
            _context.Files.Update(file);
            _context.SaveChanges();
        }

        public void UpdateLastTimeUsed(string id)
        {
            var file = GetFileById(id);
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            file.LastTimeUsed = DateTime.Now;
            _context.Files.Update(file);
            _context.SaveChanges();
        }
    }
}
