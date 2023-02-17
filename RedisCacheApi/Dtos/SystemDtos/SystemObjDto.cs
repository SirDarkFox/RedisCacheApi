namespace RedisCacheApi.Dtos.SystemDtos
{
    public class SystemObjDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Content { get; set; }
        public IEnumerable<SystemObjDto>? Folders { get; set; }
        public IEnumerable<SystemObjDto>? Files { get; set; }
    }
}
