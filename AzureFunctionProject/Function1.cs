using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionProject
{
    public static class Function1
    {
        private const int BiggestNumber = 180000;

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string responseMessage = JsonSerializer.Serialize(PrimeNumberLessThanInput(BiggestNumber));

            return new OkObjectResult(responseMessage);
        }

        private static PrimeNumbersResult PrimeNumberLessThanInput(int input)
        {
            var result = new PrimeNumbersResult {PrimeNumbers = new List<int>()};

            for (var i = 2; i < input; i++)
            {
                if (IsPrime(i))
                {
                    result.PrimeNumbers.Add(i);
                }
            }

            result.Message = $"found {result.PrimeNumbers.Count} prime numbers less than {input}";

            return result;
        }

        private static bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            for (var i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private class PrimeNumbersResult
        {
            public string Message { get; set; }

            public List<int> PrimeNumbers { get; set; }
        }
    }
}
