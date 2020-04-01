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

        // GET api/test/{times}
        [HttpGet("{times}")]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult<List<Result>> GetResults(List<int> times)
        {
            try
            {
                Result result = testService.GetTestResult();
                return Ok(result);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
