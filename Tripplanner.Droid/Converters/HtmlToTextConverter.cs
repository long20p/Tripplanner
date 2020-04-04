using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using MvvmCross.Converters;

namespace Tripplanner.Droid.Converters
{
    public class HtmlToTextConverter : MvxValueConverter<string>
    {
        protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return Html.FromHtml(value);
        }
    }
}