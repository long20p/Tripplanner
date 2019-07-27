using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Messages
{
    public class NewAccommodationCreatedMessage : MessageBase
    {
        public NewAccommodationCreatedMessage(object sender) : base(sender)
        {
        }
    }
}
