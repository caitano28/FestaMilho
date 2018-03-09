using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using FestaMilho.Renderers;
using FestaMilho.Droid;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace FestaMilho.Droid
{
    public class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundResource(Resource.Drawable.Entry_Round);
   
            }
        }
    }
}