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
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Tripplanner.Business.ViewModels;

namespace Tripplanner.Droid.Dialogs
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(TransportEditDialog))]
    public class TransportEditDialog : DialogBase<TransportEditViewModel>
    {
        protected override int DialogId => Resource.Layout.dialog_transport_entry;
    }
}