using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using Tripplanner.Business;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;
using Tripplanner.Business.Utils;
using Tripplanner.Business.ViewModels;
using Tripplanner.Droid.Services;

namespace Tripplanner.Droid.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_content_frame, true)]
    [Register("tripplanner.droid.fragments.NewTripFragment")]
    public class NewTripFragment : FragmentBase<NewTripViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_new_trip;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            this.ViewModel.OnNavigateToTripDetails = () =>
            {
                this.Activity.SupportFragmentManager.PopBackStack();
            };
            
            return view;
        }
    }
}