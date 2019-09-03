using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public class Accommodation : TripRelatedEntity, ICopyable<Accommodation>
    {
        public string Address { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool UseTripDates { get; set; }
        public string Note { get; set; }

        public void CopyFrom(Accommodation other)
        {
            Address = other.Address;
            From = other.From;
            To = other.To;
            UseTripDates = other.UseTripDates;
            Note = other.Note;
        }
    }
}
