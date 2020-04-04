using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public class LocationGuide
    {
        public string Location { get; set; }
        public IEnumerable<GuideSection> Sections { get; set; }
    }
}
