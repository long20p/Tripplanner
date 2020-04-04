using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Tripplanner.Business.ViewModels;
using MvvmCross.Droid.Support.V4;
using Tripplanner.Droid.Fragments;
using Tripplanner.Droid.Adapters;
using MvvmCross;

namespace Tripplanner.Droid.Activities
{
    [Activity(Label = "GuideMainActivity")]
    public class GuideMainActivity : ActivityBase<GuideMainViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_guide_main);

            Title = "Guide";

            var viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.activity_guide_viewpager);
            if (viewPager != null)
            {
                   var fragments = new List<TripplannerFragmentStatePagerAdapter.FragmentInfo>
                {
                    new TripplannerFragmentStatePagerAdapter.FragmentInfo
                    {
                        FragmentType = typeof(WikiGuideFragment),
                        Title = "Wikitravel",
                        ViewModel = ViewModel.WikiGuideVM
                    },
                    new TripplannerFragmentStatePagerAdapter.FragmentInfo
                    {
                        FragmentType = typeof(CustomGuideFragment),
                        Title = "Custom info",
                        ViewModel = ViewModel.CustomGuideVM
                    }
                };
                viewPager.Adapter = new TripplannerFragmentStatePagerAdapter(this, SupportFragmentManager, fragments);
            }

            var tabLayout = FindViewById<TabLayout>(Resource.Id.activity_guide_tabs);
            tabLayout.SetupWithViewPager(viewPager);
        }
    }
}