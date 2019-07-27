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
    [Activity(Label = "AccommodationActivity")]
    public class AccommodationActivity : ActivityBase<AccommodationViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_accommodation);

            Title = "Where to stay";
        }
    }
}