namespace GolfLeague.Core
{
    public class GolfLeagueScoresItem
    {
        public string Id { get; set; } = string.Empty;
        public string RoundId { get; set; } = string.Empty;
        public int StartHole { get;set; }
        public DateTime RoundPlayedDate { get; set; }
        public string RoundYear { get; set; } = string.Empty;
        public string RoundNumber { get; set; } = string.Empty;
        public long CreateDate { get; set; }
       
        public string LeagueName { get; set; } = string.Empty;

        public IEnumerable<GolferScore> GolferScores { get; set; } = new List<GolferScore>();
    }
}