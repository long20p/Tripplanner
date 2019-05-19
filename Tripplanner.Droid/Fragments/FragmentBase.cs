using MvvmCross.Droid.Support.V4;
using MvvmCross.ViewModels;

namespace Tripplanner.Droid.Fragments
{
    public class FragmentBase<T> : MvxFragment<T> where T : class, IMvxViewModel
    {
    }
}