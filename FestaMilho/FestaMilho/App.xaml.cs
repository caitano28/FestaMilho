using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FestaMilho.Data;
using FestaMilho.Model;
using FestaMilho.Services;
using FestaMilho.View;
using Xamarin.Forms;

namespace FestaMilho
{
	public partial class App : Application
	{
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public static Usuario CurrentUser { get; internal set; }
        public NavigationServices navigationServices;

        private DataService dataService;

        public App ()
		{
			InitializeComponent();
            navigationServices = new NavigationServices();
            dataService = new DataService();
            var user = dataService.GetUser();
            if (user != null && user.LembrarSenha)
            {
                App.CurrentUser = user;
                MainPage = new MasterPage();
            }
            else
            {
                MainPage = new Login();
            }
			
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
