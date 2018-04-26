using FestaMilho.Services;
using FestaMilho.ViewModel;
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
	public partial class RecuperarPage : ContentPage
	{
        private NavigationServices navigationServices;
		public RecuperarPage ()
		{
            
            RecuperarViewModel recuperarViewModel = new RecuperarViewModel();
            BindingContext = recuperarViewModel;
            navigationServices = new NavigationServices();
			InitializeComponent ();
		}

        private async  void BtCancelar_Clicked(object sender, EventArgs e)
        {
           await navigationServices.SetLoginPage();
        }
    }
}