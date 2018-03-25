using Android.Content;
using FestaMilho.Droid;
using FestaMilho.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(MySearch), typeof(MySearchRenderer))]
namespace FestaMilho.Droid
{
    public class MySearchRenderer : SearchBarRenderer
    {
        public MySearchRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetBackgroundResource(Resource.Drawable.Search_Round);
                
            }
        }
    }
}