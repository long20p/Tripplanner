using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Configs
{
    public class TripsExistenceCheckResult
    {
        public IEnumerable<ArchiveEntry> Entries { get; set; }
        public bool AlreadyExists { get; set; }
    }
}
