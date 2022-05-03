using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfLeague.Core.QueueMessages
{
    public class ScorePostedQueueMessage
    {
        public string LeagueName { get; set; } = string.Empty;
        public string RoundYear { get; set; } = string.Empty;
        public string RoundId { get;set; } = string.Empty;
    }
}
