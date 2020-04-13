using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;
using Tripplanner.Business.ViewModels.Wrappers;
using Tripplanner.Business.Messages;

namespace Tripplanner.Business.ViewModels
{
    public class WikiGuideViewModel : ViewModelBase, IDataLoader
    {
        private IGuideService guideService;

        public WikiGuideViewModel(IGuideService guideService)
        {
            this.guideService = guideService;
            RefreshHtmlPageCommand = GetAsyncCommand(RefreshHtmlPage);
            IndeterminateLoading = true;
            SubscribeToEvent<SearchSuggestionMessage>(async msg =>
            {
                Destination = msg.Suggestion;
                await RefreshHtmlPage();
            });
        }

        public ICommand RefreshHtmlPageCommand { get; }

        private ObservableCollection<GuideSectionViewModel> sections;
        public ObservableCollection<GuideSectionViewModel> Sections
        {
            get => sections;
            set
            {
                sections = value;
                RaisePropertyChanged(() => Sections);
            }
        }

        private string destination;
        public string Destination
        {
            get { return destination; }
            set
            { 
                destination = value;
                RaisePropertyChanged(() => Destination);
            }
        }

        public bool IndeterminateLoading { get; }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        private bool isError;
        public bool IsError
        {
            get => isError;
            set 
            { 
                isError = value;
                RaisePropertyChanged(() => IsError);
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set 
            { 
                errorMessage = value;
                RaisePropertyChanged(() => ErrorMessage);
            }
        }

        private ObservableCollection<GuideSuggestedPlaceViewModel> placeSuggestions;
        public ObservableCollection<GuideSuggestedPlaceViewModel> PlaceSuggestions
        {
            get => placeSuggestions; 
            set 
            { 
                placeSuggestions = value;
                RaisePropertyChanged(() => PlaceSuggestions);
            }
        }

        public async Task LoadSections()
        {
            IsLoading = true;
            IsError = false;
            try
            {
                var res = await guideService.GetAllSections(Destination);
                Sections = new ObservableCollection<GuideSectionViewModel>(res.Select(x => new GuideSectionViewModel(x)));
            }
            catch (ApplicationException aex)
            {
                IsError = true;
                ErrorMessage = $"{aex.Message} Here are some suggestions:";
                var suggestions = await guideService.GetSuggestions(Destination);
                PlaceSuggestions = new ObservableCollection<GuideSuggestedPlaceViewModel>(suggestions.Select(x => new GuideSuggestedPlaceViewModel(x)).ToList());
            }
            catch (Exception)
            {
                IsError = true;
                ErrorMessage = "Something happened while looking for the destination. Can you try again later?";
            }
            IsLoading = false;
        }

        //public override void Prepare(Trip parameter)
        //{
        //    base.Prepare(parameter);
        //    Destination = Trip.Destination;
        //}

        private async Task RefreshHtmlPage()
        {
            await LoadSections();
        }
    }
}
