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
using MvvmCross;
using MvvmCross.Platforms.Android;
using Tripplanner.Business.Services;

namespace Tripplanner.Droid.Services
{
    public class DispatcherService : IDispatcherService
    {
        public void RunOnUiThread(Action action)
        {
            Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity.RunOnUiThread(action);
        }
    }
}