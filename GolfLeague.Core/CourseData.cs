using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfLeague.Core
{
    public class CourseData
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<CourseDataHole>? Holes {get;set;}
    }

    public class CourseDataHole
    {
        public int Number { get; set; }
        public int StrokeIndex { get; set; }
        public int Par { get; set; } 
    }
}
