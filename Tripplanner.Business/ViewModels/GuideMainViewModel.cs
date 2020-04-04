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
        private IGuideService guideService;

        public GuideMainViewModel(IGuideService guideService)
        {
            this.guideService = guideService;
            WikiGuideVM = new GuideViewModel(guideService);
            CustomGuideVM = new CustomGuideViewModel();
        }

        public GuideViewModel WikiGuideVM { get; }
        public CustomGuideViewModel CustomGuideVM { get; }

        public override async Task Initialize()
        {
            WikiGuideVM.Destination = Trip.Destination;
            await WikiGuideVM.LoadSections();
            await base.Initialize();
        }
    }
}
