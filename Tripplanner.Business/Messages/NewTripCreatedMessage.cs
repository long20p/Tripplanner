using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Models;
using Tripplanner.Business.ViewModels.Wrappers;

namespace Tripplanner.Business.Messages
{
    public class NewTripCreatedMessage : MessageBase
    {
        public NewTripCreatedMessage(object sender, Trip trip) : base(sender)
        {
            Trip = trip;
        }

        public Trip Trip { get; }
    }
}
