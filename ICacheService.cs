using FinbourneTest.Models.Enums;

namespace FinbourneTest
{
    public interface ICacheService
    {
        CacheInsertFeedback CacheObject(object cacheObject, string CacheKey);
        object RetrieveCachedObject(string CacheKey);
    }
}
