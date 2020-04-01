using ItsTestingTime.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ItsTestingTime.Services
{
    public class TestService
    {
        public Result GetTestResult()
        {
            using (var wc = new WebClient())
            {
                Result result = new Result();
                var sw = new Stopwatch();
                sw.Start();
                var response = wc.DownloadData("http://localhost:80/api/time");
                sw.Stop();
                string responseString = wc.Encoding.GetString(response);
                if (responseString.Contains("dateTime"))
                {
                    result.IsSuccessful = "Success";
                }
                else
                {
                    result.IsSuccessful = "Failed";
                }

                result.TimeToLastByte = $"{sw.ElapsedMilliseconds.ToString()} ms";
                return result;
            }

        }

    }
}
