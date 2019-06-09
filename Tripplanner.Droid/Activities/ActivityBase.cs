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
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Tripplanner.Business.ViewModels;

namespace Tripplanner.Droid.Activities
{
    public abstract class ActivityBase<T> : MvxAppCompatActivity<T> where T: class, IMvxViewModel
    {
        protected ActivityBase()
        {
            Messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        }

        protected IMvxMessenger Messenger { get; }
    }
}