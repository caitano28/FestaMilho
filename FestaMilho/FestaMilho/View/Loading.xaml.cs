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
	public partial class Loading : ContentPage
	{
        public Loading(bool _IsBusy)
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           App.ImInLoadingView = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.ImInLoadingView = false;
        }
    }
}