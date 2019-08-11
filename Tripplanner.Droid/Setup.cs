using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using Tripplanner.Business;
using Tripplanner.Business.Services;
using Tripplanner.Droid.Services;

namespace Tripplanner.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        //protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        //{
        //    typeof(NavigationView).Assembly,
        //    typeof(CoordinatorLayout).Assembly,
        //    typeof(FloatingActionButton).Assembly,
        //    typeof(Toolbar).Assembly,
        //    typeof(DrawerLayout).Assembly,
        //    typeof(ViewPager).Assembly
        //};

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(AndroidViewAssemblies);
        }

        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IStorageService, StorageService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IDialogService, DialogService>();
            base.InitializeFirstChance();
        }
    }
}