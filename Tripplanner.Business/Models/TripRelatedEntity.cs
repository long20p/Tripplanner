using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tripplanner.Business.Models
{
    public abstract class TripRelatedEntity : BaseEntity
    {
        public Guid TripId { get; set; }
    }
}
