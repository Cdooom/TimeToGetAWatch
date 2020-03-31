using Microsoft.AspNetCore.Mvc;

namespace ItsTestingTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        // GET api/test/{times}
        [HttpGet("{times}")]
        public ActionResult<string> Get(int times)
        {
            return "value";
        }

    }
}
