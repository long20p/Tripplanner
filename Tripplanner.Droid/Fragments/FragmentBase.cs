using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;
using Tripplanner.Business.ViewModels;

namespace Tripplanner.Droid.Fragments
{
    public abstract class FragmentBase<T> : MvxFragment<T> where T : class, IMvxViewModel
    {
        protected abstract int FragmentId { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(FragmentId, null);
        }
    }
}