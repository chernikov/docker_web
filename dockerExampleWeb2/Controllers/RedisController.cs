using Microsoft.Extensions.Caching.Distributed;

namespace dockerExampleWeb2.Controllers
{

    
    public class RedisController : Controller
    {
        private readonly IDistributedCache cache;

        public RedisController(IDistributedCache cache)
        {
            this.cache = cache;
        }

        [Route("redis")]
        [HttpGet]
        public IActionResult Get([FromQuery] string key)
        {
            var value = cache.GetString(key);
            
            if (value == null)
            {
                return NoContent();
            }
            return Ok(value);
        }



        [Route("redis")]
        [HttpPost]
        public IActionResult Post([FromQuery] string key, [FromBody] string value, [FromQuery] int seconds = 30)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds)
            };
            cache.SetString(key, value, cacheOptions);

            
            return Created("/redis?key=" + key, value);
        }



    }
}
