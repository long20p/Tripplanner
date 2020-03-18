using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.ViewModels
{
    public class GuideViewModel : TripAwareViewModelBase
    {
        public GuideViewModel()
        {
            RefreshHtmlPageCommand = GetCommand(RefreshHtmlPage);
        }

        public ICommand RefreshHtmlPageCommand { get; }
        public Action<string> LoadHtmlPage { get; set; }
        public string PageUrl { get; set; }

        private string destination;
        public string Destination
        {
            get { return destination; }
            set
            { 
                destination = value;
                PageUrl = $"https://wikitravel.org/en/{destination}";
                RaisePropertyChanged(() => Destination);
            }
        }

        public override void Prepare(Trip parameter)
        {
            base.Prepare(parameter);
            Destination = Trip.Destination;
            PageUrl = $"https://wikitravel.org/en/{Destination}";
        }

        private void RefreshHtmlPage()
        {
            LoadHtmlPage(PageUrl);
        }
    }
}
