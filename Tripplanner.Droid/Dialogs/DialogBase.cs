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
using MvvmCross.ViewModels;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Droid.Dialogs
{
    public abstract class DialogBase<T> : MvxDialogFragment<T> where T : class, IMvxViewModel, IDismissibleComponent
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ViewModel.OnFinish = Dismiss;

            return view;
        }
    }
}