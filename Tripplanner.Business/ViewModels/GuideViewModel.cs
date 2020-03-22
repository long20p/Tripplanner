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

namespace Tripplanner.Business.ViewModels
{
    public class GuideViewModel : TripAwareViewModelBase, IDataLoader
    {
        private IGuideService guideService;

        public GuideViewModel(IGuideService guideService)
        {
            this.guideService = guideService;
            RefreshHtmlPageCommand = GetCommand(RefreshHtmlPage);
            IndeterminateLoading = true;
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

        public override Task Initialize()
        {
            LoadSections();
            return base.Initialize();
        }

        private void LoadSections()
        {
            IsLoading = true;
            var res = guideService.GetAllSections(Destination).Result;
            res = res.Where(x => x.Level == 2);
            res.ToList().ForEach(guide =>
            {
                guide.HtmlContent = guideService.GetSectionByIndex(guide.PageId, guide.Index).Result;
            });
            Sections = new ObservableCollection<GuideSectionViewModel>(res.Select(x => new GuideSectionViewModel(x)));
            IsLoading = false;
        }

        public override void Prepare(Trip parameter)
        {
            base.Prepare(parameter);
            Destination = Trip.Destination;
        }

        private void RefreshHtmlPage()
        {
            LoadSections();
        }
    }
}
