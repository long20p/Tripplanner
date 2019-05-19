using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Tripplanner.Business;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;
using Tripplanner.Business.Utils;
using Tripplanner.Business.ViewModels;
using Tripplanner.Droid.Services;

namespace Tripplanner.Droid.Fragments
{
    public class NewTripFragment : FragmentBase<NewTripViewModel>
    {
        private DateTime dateFrom;
        private DateTime dateTo;

        private IStorageService storageService;
        private ISerializer serializer;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_new_trip, container, false);

            var destinationText = view.FindViewById<AutoCompleteTextView>(Resource.Id.newTrip_location_textView);

            var dateFromText = view.FindViewById<EditText>(Resource.Id.newTrip_date_from_text);
            var showCalendarFromDateBtn = view.FindViewById<ImageButton>(Resource.Id.newTrip_open_from_date_button);
            var fromCalendarWrapper = view.FindViewById<LinearLayout>(Resource.Id.newTrip_calendar_from_wrapper);
            var fromCalendar = view.FindViewById<CalendarView>(Resource.Id.newTrip_date_from_calendar);

            var dateToText = view.FindViewById<EditText>(Resource.Id.newTrip_date_to_text);
            var showCalendarToDateBtn = view.FindViewById<ImageButton>(Resource.Id.newTrip_open_to_date_button);
            var toCalendarWrapper = view.FindViewById<LinearLayout>(Resource.Id.newTrip_calendar_to_wrapper);
            var toCalendar = view.FindViewById<CalendarView>(Resource.Id.newTrip_date_to_calendar);

            var createTripBtn = view.FindViewById<Button>(Resource.Id.newTrip_create_new_button);

            showCalendarFromDateBtn.Click += (sender, e) =>
            {
                ToggleVisibility(fromCalendarWrapper);
            };
            showCalendarToDateBtn.Click += (sender, e) =>
            {
                ToggleVisibility(toCalendarWrapper);
            };
            fromCalendar.DateChange += (sender, e) =>
            {
                SetDateText(dateFromText, e.Year, e.Month, e.DayOfMonth);
                dateFrom = new DateTime(e.Year, e.Month, e.DayOfMonth);
                ToggleVisibility(fromCalendarWrapper);
            };
            toCalendar.DateChange += (sender, e) =>
            {
                SetDateText(dateToText, e.Year, e.Month, e.DayOfMonth);
                dateTo = new DateTime(e.Year, e.Month, e.DayOfMonth);
                ToggleVisibility(toCalendarWrapper);
            };

            createTripBtn.Click += (sender, e) =>
            {
                var tripInfo = new TripInfo
                {
                    TripId = Guid.NewGuid().ToString(),
                    Destination = destinationText.Text,
                    DateFrom = dateFrom,
                    DateTo = dateTo
                };

                var data = serializer.Serialize(tripInfo);
                storageService.SaveTextFile(Path.Combine(tripInfo.TripId, Constants.GeneralTripInfoFile), data);
                
                Toast.MakeText(Activity, $"New trip to {tripInfo.Destination} saved!", ToastLength.Long).Show();
            };

            storageService = new StorageService();
            serializer = new Serializer();

            return view;
        }

        private void ToggleVisibility(View v)
        {
            v.Visibility = v.Visibility != ViewStates.Visible ? ViewStates.Visible : ViewStates.Gone;
        }

        private void SetDateText(TextView textView, int year, int month, int day)
        {
            textView.Text = $"{day}.{month}.{year}";
        }
    }
}