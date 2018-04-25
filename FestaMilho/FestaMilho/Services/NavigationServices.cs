using FestaMilho.Model;
using FestaMilho.View;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FestaMilho.Services
{
    public class NavigationServices
    {
        private DataService dataService;
        public NavigationServices()
        {
            dataService = new DataService();
        }
        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;
            switch (pageName)
            {
                case "Rank":
                    await App.Navigator.PushAsync(new Rank());
                    break;
                case "Cardapio":
                    await App.Navigator.PushAsync(new CardapioPage());
                    break;
                case "Stand":
                    await App.Navigator.PushAsync(new Stand());
                    break;
                case "Sobre":
                    await App.Navigator.PushAsync(new Sobre());
                    break;
                case "CardapioDetailPage":
                    await App.Navigator.PushAsync(new CardapioDetailPage());
                    break;
                case "BarracaDetailPage":
                    await App.Navigator.PushAsync(new BarracaDetailPage());
                    break;
                case "WebViewPage":
                    await App.Navigator.PushAsync(new WebViewPage());
                    break;
                case "Sair":
                    Logout();
                    break;
                default:
                    break;
            }
        }

        private void Logout()
        {
            App.CurrentUser.LembrarSenha = false;
            dataService.DeleteUser(App.CurrentUser);
            App.Navigator = new NavigationPage(new Login())
            {
                BarBackgroundColor = Color.FromHex("#038118")
            };
            App.Current.MainPage = App.Navigator;
        }
        public async Task SetLoadingPage()
        {
            await App.Navigator.PushAsync(new Loading(true));
        }
        public async Task PopPage()
        {
            await Task.Delay(5000);
            await App.Navigator.PopAsync();
        }

        internal async Task SetMainPage(Usuario usuario)
        {
            App.CurrentUser = usuario;
            await App.Navigator.PushAsync(new MasterPage());
        }
        internal async Task SetCadastroPage(Usuario usuario)
        {
          await App.Navigator.PushAsync(new CadastroPage(usuario));
        }
        internal async Task SetLoginPage()
        {
            await App.Navigator.PushAsync(new Login());
        }

        internal async Task SetRecuperarPage()
        {
            await App.Navigator.PushAsync(new RecuperarPage());
        }
    }
}
