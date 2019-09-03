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
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Droid.Dialogs
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(GenericConfirmationDialog))]
    public class GenericConfirmationDialog : DialogBase<GenericConfirmationViewModel>
    {
        protected override int DialogId => Resource.Layout.dialog_generic_confirmation;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}