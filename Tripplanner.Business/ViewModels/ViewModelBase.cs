using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Tripplanner.Business.ViewModels
{
    public abstract class ViewModelBase : MvxViewModel
    {
        protected ViewModelBase()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        protected IMvxNavigationService NavigationService { get; }
    }
}
