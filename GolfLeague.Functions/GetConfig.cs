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
using System.Linq;

namespace GolfLeague.Functions
{
    public static class GetConfig
    {
        [FunctionName("GetConfig")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{league}/config")] HttpRequest req,
            [CosmosDB(
                databaseName:"golfleague",
                collectionName:"golfleague-config",
                ConnectionStringSetting = "CosmosDbConnectionString",
                PartitionKey = "{league}"
                )] IEnumerable<GolfLeagueConfig> configItems,
            ILogger log)
        {
            return new OkObjectResult(configItems.First());
        }
    }
}
