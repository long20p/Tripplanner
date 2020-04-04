using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.ViewModels.Wrappers;

namespace Tripplanner.Business.Messages
{
    public class TransportDeletedMessage : MessageBase
    {
        public TransportDeletedMessage(TransportViewModel sender) : base(sender)
        {
            Transport = sender;
        }

        public TransportViewModel Transport { get; }
    }
}
