using dockerExampleWeb2.Models;
using dockerExampleWeb2.Options;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using Microsoft.Identity.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace dockerExampleWeb2.Controllers
{

    [Route("elastic-search")]
    public class ElasticSearchController : Controller
    {
        private readonly ElasticsearchClient client;

        public ElasticSearchController(ElasticSearchOptions options)
        {
            var settings = new ElasticsearchClientSettings(new Uri(options.Url))
                    .CertificateFingerprint(options.FingerPrint)
                    .Authentication(new BasicAuthentication(options.Username, options.Password))
                    .DefaultIndex("movies_v1");

            client = new ElasticsearchClient(settings);
        }


        [HttpGet]
        public IActionResult Search([FromQuery] string search)
        {
            var matchQuery = new MultiMatchQuery()
            {
                Fields = new string[] { "name" },
                Fuzziness = new Fuzziness(1),
                Analyzer = "standard",
                Query = search
            };
            var searchRequest = new SearchRequest()
            {
                From = 0,
                Size = 10,
                Query = matchQuery
            };
            var response = client.Search<ElasticMovie>(searchRequest);
            if (response.IsValidResponse)
            {
                var results = response.Documents.ToList();
                return Ok(results);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ElasticMovie movie) 
        {
            var response = await client.IndexAsync(movie);
            if (response.IsValidResponse)
            {
                return Created("", null);
            }
            return new StatusCodeResult(500);

            
        }


    }
}
