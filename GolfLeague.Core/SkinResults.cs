using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfLeague.Core
{
    public class SkinResults
    {
        public string Id { get; set; } = string.Empty;
        public string RoundYear { get; set; } = string.Empty;
        public string RoundNumber { get; set; }  = string.Empty;
        public int StartHole { get; set; } = 0;
        public DateTime RoundPlayedDate { get; set; }
        public string LeagueName { get; set; } = string.Empty;
        public int PerSkinValue { get; set; } = 0;
        public int CarryOverSkinMoney { get; set; } = 0;
        public SkinResult? Result { get; set; }
        public long CreateDate { get; set; }

        public SkinResultSummary? Summary { get; set; }
    }

    public class SkinResultSummary
    {
        public int TotalEntrants { get; set; }
        public IEnumerable<SkinResultSummaryHole>? Holes { get; set; }
        public int TotalSkins { get; set; }
        public decimal? TotalSkinMoney { get; set; }
        public decimal? TotalSkinMoneyPaid { get; set; }
    }

    public class SkinResultSummaryHole
    {
        public string Winner { get; set; } = string.Empty;
        public string WinnerIndex { get; set; } = string.Empty;
        public int HoleNumber { get; set; }
    }

    public class SkinResult
    {
        public string GolferName { get; set; } = string.Empty;
        public int Handicap { get; set; } = 0;
        public IEnumerable<SkinResultHole>? Holes { get; set; }

    }

    public class SkinResultHole
    {
        public int Gross { get; set; } = 0;
        public int Net { get; set; } = 0;
        public bool IsSkin { get; set; }
        public bool CancelSkin { get; set; }
        public int HoleNumber { get; set; } = 0;

    }
}
