using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross;
using MvvmCross.Commands;
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

        protected ICommand GetAsyncCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            return new MvxAsyncCommand(execute, canExecute);
        }

        protected ICommand GetAsyncCommand<TParam>(Func<TParam, Task> execute, Func<TParam, bool> canExecute = null)
        {
            return new MvxAsyncCommand<TParam>(execute, canExecute);
        }
    }

    public abstract class ViewModelBase : ViewModelBase<object>
    {
        public override void Prepare(object parameter)
        {
        }
    }
}
