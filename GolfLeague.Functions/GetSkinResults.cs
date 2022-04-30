using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using GolfLeague.Core;

namespace GolfLeague.Functions
{
    public static class GetSkinResults
    {
        [FunctionName("GetSkinResults")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{league}/skins/{roundYear:int?}/{roundNumber:int?}")] HttpRequest req,
            [CosmosDB(
                databaseName:"golfleague",
                collectionName:"golfleague-skinresults",
                ConnectionStringSetting = "CosmosDbConnectionString",
                PartitionKey = "{league}",
                SqlQuery = "SELECT * from c WHERE ((IS_NULL({roundYear})) OR (c.roundYear={roundYear})) AND ((IS_NULL({roundNumber})) OR (c.roundNumber = {roundNumber})) ORDER BY c.roundPlayedDate DESC"
                )] IEnumerable<SkinResults> skinResultsDocuments,
            ILogger log)
        {
            return new OkObjectResult(skinResultsDocuments);
        }
    }
}
