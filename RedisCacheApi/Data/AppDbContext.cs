using Microsoft.EntityFrameworkCore;
using RedisCacheApi.Models;

namespace RedisCacheApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<DbFolder> Folders{ get; set; }
        public DbSet<DbFile> Files{ get; set; }
    }
}
