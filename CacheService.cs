using FinbourneTest.Models;
using FinbourneTest.Models.Enums;

namespace FinbourneTest
{
    public class CacheService: ICacheService
    {
        protected private List<CacheItem> p_cacheItems;
        protected private int p_cacheMaximum = 10;
        public CacheService(int cacheMaximum) 
        {
            p_cacheItems = new List<CacheItem>();
            p_cacheMaximum = cacheMaximum;
        }

        public CacheInsertFeedback CacheObject(object cacheObject, string CacheKey)
        {
            lock (p_cacheItems)
            {
                if (p_cacheItems.Select(__ => __.CacheKey).Contains(CacheKey))
                {
                    return CacheInsertFeedback.KeyExistsFailure;
                }
                else
                {
                    p_cacheItems.Add(new CacheItem()
                    {
                        CacheValue = cacheObject,
                        CacheKey = CacheKey,
                        DateCached = DateTime.UtcNow,
                        LastUsed = DateTime.UtcNow
                    });
                }

                if (p_cacheItems.Count >= p_cacheMaximum)
                {
                    var cacheToClear = p_cacheItems.OrderBy(__ => __.LastUsed).First();
                    p_cacheItems.Remove(cacheToClear);
                    return CacheInsertFeedback.SuccessWithRemoval;
                }
                else
                {
                    return CacheInsertFeedback.Success;
                }
            }
        }

        public object RetrieveCachedObject(string CacheKey)
        {
            lock (p_cacheItems)
            {
                var cacheObject = p_cacheItems.Where(__ => __.CacheKey.Equals(CacheKey)).FirstOrDefault();

                if (cacheObject == null)
                    return new object();
                else
                {
                    p_cacheItems.Remove(cacheObject);
                    cacheObject.LastUsed = DateTime.UtcNow;
                    p_cacheItems.Add(cacheObject);
                    return cacheObject.CacheValue;
                }
            }
        }
    }
}
