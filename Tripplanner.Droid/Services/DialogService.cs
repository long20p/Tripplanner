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
using Tripplanner.Droid.Activities;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace Tripplanner.Droid.Services
{
    public class DialogService : IDialogService
    {
        private AlertDialog currentDialog;

        public void ShowDialog(Action onProceed, Action onCancel)
        {
            //var currentActivity = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            //var builder = new AlertDialog.Builder(currentActivity);
            
            //var inflater = Mvx.IoCProvider.Resolve<AddAccommodationActivity>().LayoutInflater;

            //builder.SetView(inflater.Inflate(Resource.Layout.dialog_accommodation_entry, null))
            //    .SetPositiveButton("Add", (sender, args) => onProceed())
            //    .SetNegativeButton("Cancel", (sender, args) =>
            //    {
            //        onCancel();
            //        currentDialog?.Cancel();
            //        currentDialog = null;
            //    });

            //currentDialog = builder.Create();
            //currentDialog.Show();
        }
    }
}