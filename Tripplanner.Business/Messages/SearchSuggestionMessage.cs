using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Messages
{
    public class SearchSuggestionMessage : MessageBase
    {
        public SearchSuggestionMessage(object sender, string suggestion) : base(sender)
        {
            Suggestion = suggestion;
        }

        public string Suggestion { get; }
    }
}
