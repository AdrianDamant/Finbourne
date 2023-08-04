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

        [HttpPost(Name = "TestAddObjectToCache")]
        public CacheInsertFeedback TestAddObjectToCache( )=> 
            _cacheService.CacheObject("ADRIAN", "UniqueCacheKey");

        [HttpGet(Name = "TestCollectStringObjectFromCache")]
        public string? TestCollectStringObjectFromCache() =>
            _cacheService.RetrieveCachedObject("UniqueCacheKey").ToString();
    }
}