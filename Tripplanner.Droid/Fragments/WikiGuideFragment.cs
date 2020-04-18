using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Tripplanner.Business.ViewModels;

namespace Tripplanner.Droid.Fragments
{
    //[MvxFragmentPresentation(typeof(GuideMainViewModel), Resource.Id.activity_guide_viewpager, true)]
    [Register("tripplanner.droid.fragments.WikiGuideFragment")]
    public class WikiGuideFragment : FragmentBase<WikiGuideViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_guide_wiki;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}