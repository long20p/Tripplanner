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
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Tripplanner.Droid.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : MvxSplashScreenAppCompatActivity
    {
        public SplashActivity() : base(Resource.Layout.splash)
        { }
    }
}