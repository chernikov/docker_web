namespace dockerExampleWeb2.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Get()
        {
            var result = new int[] { 1, 2 };
            return Ok(result);
        }
    }
}
