namespace FinbourneTest.Models
{
    public class CacheItem
    {
        public required object CacheValue { get; set; }
        public required string CacheKey { get; set; }
        public DateTime DateCached { get; set; }
        public DateTime LastUsed { get; set; }
    }
}
