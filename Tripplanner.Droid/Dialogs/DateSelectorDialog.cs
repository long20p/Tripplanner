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
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Droid.Dialogs
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(DateSelectorDialog))]
    public class DateSelectorDialog : DialogBase<DateSelectorViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.dialog_date_selector, null);

            var calendar = view.FindViewById<CalendarView>(Resource.Id.dialog_date_selector_calendar);
            calendar.DateChange += (sender, e) =>
            {
                ViewModel.SelectedDate = new DateTime(e.Year, e.Month + 1, e.DayOfMonth);
            };

            return view;
        }
    }
}