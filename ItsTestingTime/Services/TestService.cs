using ItsTestingTime.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
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
                result.StartTime = DateTime.Now.ToLongTimeString();
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
                Console.WriteLine(responseString);
                return result;
            }

        }

        public void GetLoadTestResults(int runsPerSecond)
        {
            Task[] taskArray = new Task[runsPerSecond];
            List<Result> resultArray = new List<Result>();
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {
                    Result result = obj as Result;
                    result = GetTestResult();
                    if (result == null)
                        return;
                    result.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                    resultArray.Add(result);

                }, CancellationToken.None);
            }
            Task.WaitAll(taskArray);
            foreach (var item in resultArray)
            {
                if (item != null)
                    Console.WriteLine("Task #{0} created at #{1}, ran #{2} for #{3}.",
                                      item.ThreadNum, item.StartTime, item.IsSuccessful, item.TimeToLastByte);
            }
        }

        //Method to Call Load Test Function. Sleep every second. Add to another result array. Print out
        //Method to calculate average of array.
    }
}
