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
    public static class SaveScores
    {
        [FunctionName("SaveScores")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "{league}/scores")] HttpRequest req,
            string league,
            [CosmosDB(
                databaseName:"golfleague",
                collectionName:"golfleague-scores",
                ConnectionStringSetting = "CosmosDbConnectionString",
                CreateIfNotExists = false
            )] out GolfLeagueScoresItem outputDocument,
            [Queue("%ScorePostedQueueName%")] out dynamic queueMessage,
            ILogger log)
        {
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = streamReader.ReadToEnd();
            }
            var leagueScoreItem = JsonConvert.DeserializeObject<GolfLeagueScoresItem>(requestBody);
            leagueScoreItem.RoundYear = leagueScoreItem.RoundId.Substring(0, 4);
            leagueScoreItem.RoundNumber = leagueScoreItem.RoundId.Substring(4);
            leagueScoreItem.Id = leagueScoreItem.RoundId;
            leagueScoreItem.CreateDate = DateTime.Now.ToFileTimeUtc();
            leagueScoreItem.LeagueName = league;
            outputDocument = leagueScoreItem;
            queueMessage = new
            {
                leagueName = league,
                roundYear = leagueScoreItem.RoundYear,
                roundId = leagueScoreItem.RoundId
            };
            return new OkResult();
        }
    }
}
