using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tripplanner.Business.Models
{
    public abstract class TripRelatedEntity : BaseEntity
    {
        [Indexed]
        public Guid TripId { get; set; }
    }
}
