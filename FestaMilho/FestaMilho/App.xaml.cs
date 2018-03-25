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


        private DataService dataService;

        public App ()
		{
			InitializeComponent();

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
