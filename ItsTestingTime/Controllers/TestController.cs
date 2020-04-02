using ItsTestingTime.Models;
using ItsTestingTime.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public ActionResult<List<Result>> GetSingleLoadResult(int runs)
        {
            try
            {
                List<Result> runResults = testService.RunConcurrentCalls(runs).Result;
                return Ok(runResults);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/test/{iterations}/{runs}}
        [HttpGet("{iterations}/{runs}")]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult<List<Result>> GetMultipleLoadResults(int iterations, int runs)
        {
            try
            {
                List<Result> runResults = testService.SimulateRunsOverTime(iterations, runs).Result;
                return Ok(runResults);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/test/summary/{iterations}/{runs}}
        [HttpGet("summary/{iterations}/{runs}")]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult<List<Result>> GetMultipleLoadResultsWithSummary(int iterations, int runs)
        {
            try
            {
                Summary summary = testService.SimulateRunsOverTimeWithSummary(iterations, runs);
                return Ok(summary);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
