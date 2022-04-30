using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfLeague.Core
{
    public class GolfLeagueConfig
    {
        public int Id { get; set; }
        public CourseData? CourseData { get; set; }
        public long CreateDate { get; set; }

        public string LeagueName { get; set; } = string.Empty;
    }
}
