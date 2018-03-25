using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FestaMilho.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BarracaDetailPage : ContentPage
	{
		public BarracaDetailPage ()
		{
			InitializeComponent ();
            ImgLocal.Source = String.Format("mapa{0}.png", localizacao.Text);
        }
	}
}