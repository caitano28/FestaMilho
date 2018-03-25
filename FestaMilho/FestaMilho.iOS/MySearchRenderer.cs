using FestaMilho.iOS;
using FestaMilho.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(MySearch), typeof(MySearchRenderer))]
namespace FestaMilho.iOS
{
    public class MySearchRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
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