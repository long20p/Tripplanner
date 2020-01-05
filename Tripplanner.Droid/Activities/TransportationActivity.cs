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
    [Activity(Label = "TransportationActivity")]
    public class TransportationActivity : ActivityBase<TransportationListViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_transportation);

            Title = "Get there";
        }
    }
}