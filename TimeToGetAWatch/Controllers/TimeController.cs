using Microsoft.AspNetCore.Http;
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
            try
            {
                DateTime currentDateTime = DateTime.Now;
                string now = currentDateTime.ToString();
                return Ok(new Time
                {
                    DateTime = now
                });
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

    }
}
