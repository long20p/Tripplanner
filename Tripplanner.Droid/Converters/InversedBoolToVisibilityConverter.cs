using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Converters;

namespace Tripplanner.Droid.Converters
{
    public class InversedBoolToVisibilityConverter : MvxValueConverter<bool, ViewStates>
    {
        protected override ViewStates Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? ViewStates.Gone : ViewStates.Visible;
        }
    }
}