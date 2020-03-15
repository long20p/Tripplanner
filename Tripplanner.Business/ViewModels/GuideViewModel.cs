using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.ViewModels
{
    public class GuideViewModel : TripAwareViewModelBase
    {
        public Action<string> LoadHtmlPage { get; set; }
        public string PageUrl { get; set; }

        public override void Prepare(Trip parameter)
        {
            base.Prepare(parameter);
            PageUrl = $"https://wikitravel.org/en/{Trip.Destination}";
        }
    }
}
