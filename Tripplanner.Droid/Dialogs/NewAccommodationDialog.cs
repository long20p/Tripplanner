using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Tripplanner.Business.ViewModels;

namespace Tripplanner.Droid.Dialogs
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(NewAccommodationDialog))]
    public class NewAccommodationDialog : DialogBase<NewAccommodationViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.dialog_accommodation_entry, null);
            return view;
        }
    }
}