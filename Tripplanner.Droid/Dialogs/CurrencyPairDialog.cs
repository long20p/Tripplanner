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
    [Register(nameof(CurrencyPairDialog))]
    public class CurrencyPairDialog : DialogBase<NewCurrencyPairViewModel>
    {
        protected override int DialogId => Resource.Layout.dialog_currency_entry;

        protected override double RelativeWidth => 0.6;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}