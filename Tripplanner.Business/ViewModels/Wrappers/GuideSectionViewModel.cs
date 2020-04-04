using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.ViewModels.Wrappers
{
    public class GuideSectionViewModel : ViewModelBase
    {
        private GuideSection section;

        public GuideSectionViewModel(GuideSection section)
        {
            this.section = section;
            SelectSectionCommand = GetCommand(SelectSection);
        }

        public ICommand SelectSectionCommand { get; }

        public GuideSection Section => section;

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set 
            { 
                isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        private void SelectSection()
        {
            IsSelected = !IsSelected;
        }
    }
}
