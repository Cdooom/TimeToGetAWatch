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

        // GET api/test/{runs}}
        [HttpGet("{runs}")]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult<List<Result>> GetLoadTestResults(int runs)
        {
            try
            {
                List<Result> runResults = testService.GetLoadTestResults(runs);
                return Ok(runResults);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
