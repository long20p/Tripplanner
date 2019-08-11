using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Messages
{
    public class NewTripCreatedMessage : MessageBase
    {
        public NewTripCreatedMessage(object sender, string destination) : base(sender)
        {
            Destination = destination;
        }

        public string Destination { get; }
    }
}
