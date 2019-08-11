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
using Tripplanner.Business.ViewModels;

namespace Tripplanner.Droid.Activities
{
    [Activity(Label = "TripDetailsActivity")]
    public class TripDetailsActivity : ActivityBase<TripDetailsViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_trip_details);

            Title = ViewModel.Trip.Destination;
        }
    }
}