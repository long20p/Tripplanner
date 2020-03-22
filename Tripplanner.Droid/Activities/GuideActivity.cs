using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Tripplanner.Business.ViewModels;

namespace Tripplanner.Droid.Activities
{
    [Activity(Label = "GuideActivity")]
    public class GuideActivity : ActivityBase<GuideViewModel>
    {
        private WebView guideView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.activity_guide);
            //guideView = FindViewById<WebView>(Resource.Id.wikitravelWebView);
            //ViewModel.LoadHtmlPage = RefreshWebView;
            //RefreshWebView(ViewModel.PageUrl);

            Title = "Guide";
        }

        private void RefreshWebView(string url)
        {
            guideView.LoadUrl(url);
            //guideView.LoadData(url, "text/html", null);
        }
    }
}