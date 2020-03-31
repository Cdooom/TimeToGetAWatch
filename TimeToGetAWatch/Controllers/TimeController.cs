using Microsoft.AspNetCore.Mvc;
using System;
using TimeToGetAWatch.Models;

namespace TimeToGetAWatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        // GET api/time
        [HttpGet()]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult<Time> Get()
        {
            DateTime currentDateTime = DateTime.Now;
            string now = currentDateTime.ToString();
            return new Time {
                DateTime = now
            };
        }

    }
}
