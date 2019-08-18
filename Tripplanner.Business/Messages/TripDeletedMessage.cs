using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.ViewModels.Wrappers;

namespace Tripplanner.Business.Messages
{
    public class TripDeletedMessage : MessageBase
    {
        public TripDeletedMessage(TripViewModel sender) : base(sender)
        {
            Trip = sender;
        }

        public TripViewModel Trip { get; }
    }
}
