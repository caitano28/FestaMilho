using FestaMilho.Model;
using FestaMilho.View;
using System;
using System.Threading.Tasks;

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
            App.Current.MainPage = new Login();
        }

        internal void SetMainPage(Usuario usuario)
        {
            App.CurrentUser = usuario;
            App.Current.MainPage = new MasterPage();
        }
        internal void SetCadastroPage(Usuario usuario)
        {
           App.Current.MainPage = new CadastroPage(usuario);
        }
        internal void SetLoginPage()
        {
            App.Current.MainPage = new Login();
        }

        internal void SetRecuperarPage()
        {
            App.Current.MainPage = new RecuperarPage();
        }
    }
}
