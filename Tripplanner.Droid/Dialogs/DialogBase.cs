using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Droid.Dialogs
{
    public abstract class DialogBase<T> : MvxDialogFragment<T> where T : class, IMvxViewModel, IDismissibleComponent
    {
        protected abstract int DialogId { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(DialogId, null);
            ViewModel.OnFinish = Dismiss;
            Dialog.SetCanceledOnTouchOutside(false);
            return view;
        }
    }
}