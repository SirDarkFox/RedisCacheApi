using Microsoft.EntityFrameworkCore;
using RedisCacheApi.Models;
using RedisCacheApi.Utility;

namespace RedisCacheApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string? folderId1 = ModelGenerator.GetFolderId();
            string? folderId2 = ModelGenerator.GetFolderId();
            string? folderId3 = ModelGenerator.GetFolderId();
            string? folderId4 = ModelGenerator.GetFolderId();

            string? fileId1 = ModelGenerator.GetFileId();
            string? fileId2 = ModelGenerator.GetFileId();
            string? fileId3 = ModelGenerator.GetFileId();
            string? fileId4 = ModelGenerator.GetFileId();
            string? fileId5 = ModelGenerator.GetFileId();
            string? fileId6 = ModelGenerator.GetFileId();
            string? fileId7 = ModelGenerator.GetFileId();

            modelBuilder.Entity<DbFolder>().HasData(new[]
            {
                new { Id = folderId1, Name = "Folder 1", FolderForeignId = (string?)null },
                new { Id = folderId2, Name = "Folder 2", FolderForeignId = folderId1 },
                new { Id = folderId3, Name = "Folder 3", FolderForeignId = folderId2 },
                new { Id = folderId4, Name = "Folder 4", FolderForeignId = folderId2 }
            });

            modelBuilder.Entity<DbFile>().HasData(new[]
            {
                new { Id = fileId1, Name = "File 1", Content = "File 1 Content", LastTimeUsed = default(DateTime), FolderForeignId = (string?)null },
                new { Id = fileId2, Name = "File 2", Content = "File 2 Content", LastTimeUsed = default(DateTime), FolderForeignId = folderId1 },
                new { Id = fileId3, Name = "File 3", Content = "File 3 Content", LastTimeUsed = default(DateTime), FolderForeignId = folderId1 },
                new { Id = fileId4, Name = "File 4", Content = "File 4 Content", LastTimeUsed = default(DateTime), FolderForeignId = folderId2 },
                new { Id = fileId5, Name = "File 5", Content = "File 5 Content", LastTimeUsed = default(DateTime), FolderForeignId = folderId3 },
                new { Id = fileId6, Name = "File 6", Content = "File 6 Content", LastTimeUsed = default(DateTime), FolderForeignId = folderId3 },
                new { Id = fileId7, Name = "File 7", Content = "File 7 Content", LastTimeUsed = default(DateTime), FolderForeignId = folderId3 }
            });
        }

        public DbSet<DbFolder> Folders{ get; set; }
        public DbSet<DbFile> Files{ get; set; }
    }
}
