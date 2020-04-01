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
                try
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
                catch(Exception ex)
                {
                    return new Result() { IsSuccessful = "Failed", TimeToLastByte = "0" , Exception = $"{ex.ToString()}"};
                }
            }
        }

        public async Task<List<Result>> RunConcurrentCalls (int concurrentHits)
        {

            Task[] taskArray = new Task[concurrentHits];
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
                    Console.WriteLine("Task #{0} created at {1}, ran {2} for {3}.",
                                        item.ThreadNum, item.StartTime, item.IsSuccessful, item.TimeToLastByte);
            }
            Console.WriteLine($"Executed {resultArray.Count} concurrently");
            return resultArray;
        }

        public async Task<List<Result>> SimulateRunsOverTime (int seconds, int concurrentHits)
        {
            Task[] taskArray = new Task[seconds];
            List<Result> resultArray = new List<Result>();
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {
                    List<Result> result = obj as List<Result>;
                    result = RunConcurrentCalls(concurrentHits).Result;
                    if (result.Count == 0)
                        resultArray = result;
                    else
                    {
                        resultArray.AddRange(result);
                    }

                }, CancellationToken.None);

            }
            Task.WaitAll(taskArray);

            Console.WriteLine($"Executed total of {resultArray.Count} over {seconds} seconds");
            return resultArray;
        }

        public Summary SimulateRunsOverTimeWithSummary (int seconds, int concurrentHits)
        {
            List<Result> resultArray = new List<Result>();
            resultArray = SimulateRunsOverTime(seconds, concurrentHits).Result;
            int successes = 0;
            int failures = 0;
            int totalttlb = 0;
            int averagettlb;
            foreach(Result result in resultArray)
            {
                if(result.IsSuccessful == "Success")
                {
                    successes += 1;
                }
                if (result.IsSuccessful == "Failed")
                {
                    failures += 1;
                }

                int ttlb = int.Parse(result.TimeToLastByte.Split("ms")[0]);
                totalttlb += ttlb;
            }

            averagettlb = totalttlb / resultArray.Count;

            return new Summary() { TotalSuccesses = successes, TotalFailures = failures, AverageTimeToLastByte = $"{averagettlb.ToString()} ms" };
        }
      
    }
}
