﻿using FestaMilho.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FestaMilho.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
          
            LoginViewModel loginViewModel = new LoginViewModel();
            BindingContext = loginViewModel;
			InitializeComponent ();
		}
	}
}