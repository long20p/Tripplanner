using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace Tripplanner.Business.ViewModels
{
    public abstract class ViewModelBase<T> : MvxViewModel<T>
    {
        protected ViewModelBase()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            Messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        }

        protected IMvxNavigationService NavigationService { get; }
        protected IMvxMessenger Messenger { get; }
    }

    public abstract class ViewModelBase : ViewModelBase<object>
    {
        public override void Prepare(object parameter)
        {
        }
    }
}
