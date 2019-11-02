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
using Tripplanner.Business.Messages;

namespace Tripplanner.Business.ViewModels
{
    public abstract class ViewModelBase<T> : MvxViewModel<T>
    {
        private readonly IMvxMessenger messenger;

        protected ViewModelBase()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        }

        protected IMvxNavigationService NavigationService { get; }
        

        protected ICommand GetAsyncCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            return new MvxAsyncCommand(execute, canExecute);
        }

        protected ICommand GetAsyncCommand<TParam>(Func<TParam, Task> execute, Func<TParam, bool> canExecute = null)
        {
            return new MvxAsyncCommand<TParam>(execute, canExecute);
        }

        protected ICommand GetCommand(Action execute, Func<bool> canExecute = null)
        {
            return new MvxCommand(execute, canExecute);
        }

        protected ICommand GetCommand<TParam>(Action<TParam> execute, Func<TParam, bool> canExecute = null)
        {
            return new MvxCommand<TParam>(execute, canExecute);
        }

        protected void PublishEvent<TParam>(TParam message) where TParam : MessageBase
        {
            messenger.Publish(message);
        }

        protected void SubscribeToEvent<TParam>(Action<TParam> action) where TParam : MessageBase
        {
            messenger.Subscribe(action, MvxReference.Strong);
        }
    }

    public abstract class ViewModelBase : ViewModelBase<object>
    {
        public override void Prepare(object parameter)
        {
        }
    }
}
