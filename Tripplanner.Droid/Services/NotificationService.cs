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
    public class NotificationService : INotificationService
    {
        public void ShowInfo(string message)
        {
            Toast.MakeText(Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity, message, ToastLength.Long)
                .Show();
        }
    }
}