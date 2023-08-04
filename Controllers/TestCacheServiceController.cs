using Microsoft.AspNetCore.Mvc;
using FinbourneTest.Models.Enums;

namespace FinbourneTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestCacheServiceController : Controller
    {
        private ICacheService _cacheService;
        private readonly ILogger<TestCacheServiceController> _logger;

        public TestCacheServiceController(ILogger<TestCacheServiceController> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }


        [HttpPost]
        [Route(nameof(TestAddSingleObjectToCache))]
        public CacheInsertFeedback TestAddSingleObjectToCache( )=> 
            _cacheService.CacheObject("ADRIAN", "UniqueCacheKey");

        [HttpPost]
        [Route(nameof(TestAddMultipleItems))]
        public CacheInsertFeedback TestAddMultipleItems()
        {
            Random rnd = new Random();
            return _cacheService.CacheObject("ADRIAN" + rnd.Next(100), 
                "UniqueCacheKey" + rnd.Next(100));
        }

        [HttpGet]
        [Route(nameof(TestCollectStringObjectFromCache))]
        public string? TestCollectStringObjectFromCache() =>
            _cacheService.RetrieveCachedObject("UniqueCacheKey").ToString();
    }
}