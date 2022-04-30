using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GolfLeague.Core;
using System.Collections.Generic;

namespace GolfLeague.Functions
{
    public static class GetScores
    {
        [FunctionName("GetScores")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "{league}/scores/{roundYear:int?}/{roundNumber:int?}")] HttpRequest req,
            [CosmosDB(
                databaseName:"golfleague",
                collectionName:"golfleague-scores",
                ConnectionStringSetting = "CosmosDbConnectionString",
                PartitionKey = "{league}",
                SqlQuery = "SELECT * from c WHERE ((IS_NULL({roundYear})) OR (c.roundYear={roundYear})) AND ((IS_NULL({roundNumber})) OR (c.roundNumber = {roundNumber})) ORDER BY c.roundPlayedDate DESC"
                )] out GolfLeagueScoresItem scoreItem,
            ILogger log)
        {
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = streamReader.ReadToEnd();
            }
            scoreItem = JsonConvert.DeserializeObject<GolfLeagueScoresItem>(requestBody);
            return new OkResult();
        }
    }
}
