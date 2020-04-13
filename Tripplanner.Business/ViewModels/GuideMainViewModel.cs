using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels
{
    public class GuideMainViewModel : TripAwareViewModelBase
    {
        public GuideMainViewModel()
        {
            WikiGuideVM = ResolveType<WikiGuideViewModel>();
            CustomGuideVM = ResolveType<CustomGuideViewModel>();
        }

        public WikiGuideViewModel WikiGuideVM { get; }
        public CustomGuideViewModel CustomGuideVM { get; }

        public override async Task Initialize()
        {
            WikiGuideVM.Destination = Trip.Destination;
            await WikiGuideVM.LoadSections();
            CustomGuideVM.LoadTripNotes(Trip);
            await base.Initialize();
        }
    }
}
