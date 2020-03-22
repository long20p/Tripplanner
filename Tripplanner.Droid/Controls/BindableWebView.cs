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
using Android.Webkit;
using Android.Widget;

namespace Tripplanner.Droid.Controls
{
    public class BindableWebView : WebView
    {
        public BindableWebView(Context context) : base(context)
        {
        }

        public BindableWebView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public BindableWebView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public BindableWebView(Context context, IAttributeSet attrs, int defStyleAttr, bool privateBrowsing) : base(context, attrs, defStyleAttr, privateBrowsing)
        {
        }

        public BindableWebView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected BindableWebView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        private string htmlData;
        public string HtmlData
        {
            get => htmlData;
            set
            {
                htmlData = value;
                LoadData(htmlData, "text/html; charset=UTF-8", null);
            }
        }
    }
}