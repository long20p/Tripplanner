using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Tripplanner.Business.Messages;

namespace Tripplanner.Business.ViewModels.Wrappers
{
    public class GuideSuggestedPlaceViewModel : ViewModelBase
    {
        public GuideSuggestedPlaceViewModel(string suggestion)
        {
            Suggestion = suggestion;
            SearchBySuggestionCommand = GetCommand(SearchBySuggestion);
        }

        public string Suggestion { get; }

        public ICommand SearchBySuggestionCommand { get; }

        private void SearchBySuggestion()
        {
            PublishEvent(new SearchSuggestionMessage(this, Suggestion));
        }
    }
}
