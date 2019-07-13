using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Messages
{
    public class TripDeletedMessage : MessageBase
    {
        public TripDeletedMessage(object sender) : base(sender)
        {
        }
    }
}
