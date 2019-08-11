using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.ViewModels.Wrappers;

namespace Tripplanner.Business.Messages
{
    public class AccommodationDeletedMessage : MessageBase
    {
        public AccommodationDeletedMessage(AccommodationViewModel sender) : base(sender)
        {
            Accommodation = sender;
        }

        public AccommodationViewModel Accommodation { get; }
    }
}
