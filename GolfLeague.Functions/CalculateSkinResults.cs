using System;
using System.Collections.Generic;
using GolfLeague.Core;
using GolfLeague.Core.QueueMessages;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GolfLeague.Functions
{
    public class CalculateSkinResults
    {
        [FunctionName("CalculateSkinResults")]
        public void Run(
            [QueueTrigger("%dScorePostedQueueName%", Connection = "GolfLeagueStoreAccountConnectionString")]string myQueueItem,
            [CosmosDB(
                databaseName:"golfleague",
                collectionName:"golfleague-config",
                ConnectionStringSetting = "CosmosDbConnectionString",
                PartitionKey = "{league}"
                )] IEnumerable<GolfLeagueConfig> configItems,
            [CosmosDB(
               databaseName:"golfleague",
            collectionName:"golfleague-scores",
            PartitionKey = "{leagueName}",
            ConnectionStringSetting ="CosmosDbConnectionString",
            Id = "{roundId}"
            )] GolfLeagueScoresItem golfLeagueScores,
            [CosmosDB(
                databaseName:"golfleague",
                collectionName:"golfleague-skinresults",
                ConnectionStringSetting = "CosmosDbConnectionString",
                PartitionKey = "{leagueName}",
                SqlQuery = "SELECT * FROM c WHERE c.roundNumber = ToString(StringToNumber(SUBSTRING({ roundId},4,2)) - 1) AND c.roundYear = SUBSTRING({ roundId},0,4)"
            )] SkinResults lastWeeksScores,
            [CosmosDB(
                databaseName:"golfleague",
                collectionName:"golfleague-skinresults",
                ConnectionStringSetting = "CosmosDbConnectionString",
                CreateIfNotExists = false,
                PartitionKey = "{leagueName}"
            )] out SkinResults skinResultsDocument,
            [Queue("%SkinResultupdateQueueName%", Connection = "GolfLeagueStoreAccountConnectionString")] out SkinResultUpdateQueueName queueMessage,
            ILogger log)
        {
            skinResultsDocument = new SkinResults();
            queueMessage = new SkinResultUpdateQueueName();
        }
    }
}
