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
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_content_frame, true)]
    [Register("tripplanner.droid.fragments.BackupFragment")]
    public class BackupFragment : FragmentBase<BackupViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_backup;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}