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
namespace GolfLeague.Functions
{
    public static class SaveConfig
    {
        [FunctionName("SaveConfig")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "{league}/config")] HttpRequest req,
            [CosmosDB(
                databaseName:"golfleague",
                collectionName:"golfleague-config",
                ConnectionStringSetting = "CosmosDbConnectionString",
                CreateIfNotExists = false
            )] out GolfLeagueConfig outputDocument,
            ILogger log)
        {
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = streamReader.ReadToEnd();
            }
            outputDocument = JsonConvert.DeserializeObject<GolfLeagueConfig>(requestBody);
            return new OkResult();
        }
    }
}
