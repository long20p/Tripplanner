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
using MvvmCross.ViewModels;

namespace Tripplanner.Droid.Activities
{
    public abstract class ActivityBase<T> : MvxAppCompatActivity<T> where T: class, IMvxViewModel
    {
    }
}