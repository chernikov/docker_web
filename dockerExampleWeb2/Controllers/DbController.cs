
using dockerExampleWeb2.Models;
using Microsoft.EntityFrameworkCore;

namespace dockerExampleWeb2.Controllers
{
    [Route("db")]
    public class DbController : Controller
    {
        private readonly SchoolContext dbContext;

        public DbController(SchoolContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = dbContext.Students.ToList();
            return Ok(list);
        }
    }
}
