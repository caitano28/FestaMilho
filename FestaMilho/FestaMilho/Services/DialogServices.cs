using System.Threading.Tasks;

namespace FestaMilho.Services
{
    public class DialogServices
    {
        public async Task ShowMessage(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Ok");
        }
    }
}
