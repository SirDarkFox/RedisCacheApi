namespace RedisCacheApi.Utility
{
    public static class CacheCheck
    {
        public static bool UseQuantityCheck(DateTime dateTime) => (DateTime.Now - dateTime).TotalHours <= 3;
        public static bool ExpirationCheck(DateTime dateTime) => (DateTime.Now - dateTime).TotalHours <= 6;
    }
}
