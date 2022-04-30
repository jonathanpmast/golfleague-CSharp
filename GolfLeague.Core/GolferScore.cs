using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfLeague.Core
{
    public class GolferScore
    {
        public string GolferName { get; set; } = string.Empty;
        public int Handicap { get; set; }
        public IEnumerable<int>? Scores { get; set; } 
        public bool InSkins { get; set; }
    }
}
