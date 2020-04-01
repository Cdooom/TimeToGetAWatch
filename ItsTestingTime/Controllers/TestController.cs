using ItsTestingTime.Models;
using ItsTestingTime.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ItsTestingTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private TestService testService = new TestService();

        // GET api/test}
        [HttpGet()]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult<List<Result>> GetResults()
        {
            try
            {
                testService.GetLoadTestResults(100);
                return Ok();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
