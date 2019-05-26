using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using V4App = Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Tripplanner.Business.ViewModels;
using Tripplanner.Droid.Fragments;

namespace Tripplanner.Droid.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/Tripplanner.AppTheme.NoActionBar")]
    public class MainActivity : ActivityBase<MainViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private Dictionary<int, string> viewModelMappings = new Dictionary<int, string>
        {
            {Resource.Id.nav_new_trip, nameof(NewTripViewModel)},
            {Resource.Id.nav_all_trips, nameof(AllTripsViewModel)},
            {Resource.Id.nav_settings, nameof(SettingsViewModel)},
            {Resource.Id.nav_backup, nameof(BackupViewModel)},
            {Resource.Id.nav_about, nameof(AboutViewModel)}
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            //Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            //DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            //drawer.AddDrawerListener(toggle);
            //toggle.SyncState();

            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            var contentFrame = FindViewById<FrameLayout>(Resource.Id.main_content_frame);

            if(savedInstanceState == null)
            {
                NavigateToPage(Resource.Id.nav_all_trips);
                navigationView.SetCheckedItem(Resource.Id.nav_all_trips);
            }
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                if (SupportFragmentManager.BackStackEntryCount == 1)
                {
                    SupportFragmentManager.PopBackStackImmediate();
                }
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetCheckedItem(item.ItemId);

            NavigateToPage(item.ItemId);

            var drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            
            return true;
        }

        private void NavigateToPage(int pageId)
        {
            var viewModelName = viewModelMappings[pageId];
            ViewModel.NavigateToPage(viewModelName);
            //V4App.Fragment fragment = null;

            //if (pageId == Resource.Id.nav_new_trip)
            //{
            //    fragment = new NewTripFragment();
            //}
            //else if (pageId == Resource.Id.nav_all_trips)
            //{
            //    fragment = new AllTripsFragment();
            //}
            //else if (pageId == Resource.Id.nav_backup)
            //{

            //}
            //else if (pageId == Resource.Id.nav_restore)
            //{

            //}
            //else if (pageId == Resource.Id.nav_settings)
            //{

            //}
            //else if (pageId == Resource.Id.nav_about)
            //{

            //}

            //SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main_content_frame, fragment).Commit();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

