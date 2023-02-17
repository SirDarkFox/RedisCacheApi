using RedisCacheApi.Models;

namespace RedisCacheApi.Utility
{
    public static class ModelGenerator
    {
        public static string? GetFolderId() => $"Folder:{Guid.NewGuid()}";
        public static string? GetFileId() => $"File:{Guid.NewGuid()}";

        public static DbFolder GenerateFolder()
        {
            string? id = GetFolderId();
            string name = Guid.NewGuid().ToString();

            var folderModel = new DbFolder { Id = id ?? "", Name = name, FolderForeign = null };

            return folderModel;
        }

        public static DbFile GenerateFile()
        {
            string? id = GetFolderId();
            string name = Guid.NewGuid().ToString();
            string content = Guid.NewGuid().ToString();

            var fileModel = new DbFile { Id = id ?? "", Name = name, Content = content, LastTimeUsed = default, FolderForeign = null };

            return fileModel;
        }
    }
}
