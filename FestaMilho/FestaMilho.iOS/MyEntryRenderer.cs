using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using UIKit;
using FestaMilho.Renderers;
using FestaMilho.iOS;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace FestaMilho.iOS
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UITextField here!
                Control.BackgroundColor = UIColor.White;
                Control.Layer.CornerRadius = 4;
                Control.Layer.BorderWidth = 1;
                Control.Layer.BorderColor = UIColor.FromRGB(238, 238, 238).CGColor;
            }
        }
    }
}