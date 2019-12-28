using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.ViewModels.Wrappers;

namespace Tripplanner.Business.Messages
{
    public class CurrencyPairDeletedMessage : MessageBase
    {
        public CurrencyPairDeletedMessage(CurrencyPairViewModel sender) : base(sender)
        {
            CurrencyPair = sender;
        }

        public CurrencyPairViewModel CurrencyPair { get; }
    }
}
