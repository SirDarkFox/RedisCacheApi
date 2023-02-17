using RedisCacheApi.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisCacheApi.Data.FileRepos
{
    public class RedisFileRepo : INoSqlFileRepo
    {
        private readonly string _fileHashSet = "hashfile";
        private readonly IConnectionMultiplexer _redis;

        public RedisFileRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;

        }

        public IEnumerable<DbFile?>? GetAllFiles()
        {
            var db = _redis.GetDatabase();
            var hashFile = db.HashGetAll(_fileHashSet);

            if (hashFile.Length == 0)
            {
                return null;
            }

            var result = Array.ConvertAll(hashFile, val =>
                JsonSerializer.Deserialize<DbFile>(val.Value)).ToList();

            return result;
        }

        public DbFile? GetFileById(string id)
        {
            var db = _redis.GetDatabase();
            var serFile = db.HashGet(_fileHashSet, id);

            if (string.IsNullOrEmpty(serFile))
            {
                return null;
            }

            return JsonSerializer.Deserialize<DbFile>(serFile);
        }

        public void CreateFile(DbFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var db = _redis.GetDatabase();
            var serFile = JsonSerializer.Serialize(file);

            db.HashSet(_fileHashSet, new HashEntry[]
                {
                    new HashEntry(file.Id, serFile)
                });
        }

        public void UpdateFile(DbFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var db = _redis.GetDatabase();
            var serFile = JsonSerializer.Serialize(file);
            db.HashSet(_fileHashSet, file.Id, serFile);
        }

        public void DeleteFile(DbFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var db = _redis.GetDatabase();
            db.HashDelete(_fileHashSet, file.Id);
        }

        public void UpdateLastTimeUsed(DbFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            file.LastTimeUsed = DateTime.Now;
            UpdateFile(file);
        }
    }
}
