using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GolfLeague.Functions
{
    public static class GetScores
    {
        [FunctionName("GetScores")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "{league}/scores/{roundYear:int?}/{roundNumber:int?}")] HttpRequest req,
            [CosmosDB(
                databaseName:"golfleague",
                collectionName:"golfleague-scores",
                ConnectionStringSetting = "CosmosDbConnectionString",
                PartitionKey = "{league}",
                SqlQuery = "SELECT * from c WHERE ((IS_NULL({roundYear})) OR (c.roundYear={roundYear})) AND ((IS_NULL({roundNumber})) OR (c.roundNumber = {roundNumber})) ORDER BY c.roundPlayedDate DESC"
                )]
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
