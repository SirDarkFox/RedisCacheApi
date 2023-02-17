namespace RedisCacheApi.Dtos.FolderDtos
{
    public class FolderReadDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? FolderId { get; set; }
        public string? FolderName { get; set; }
    }
}
