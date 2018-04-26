using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FestaMilho.Services
{
    public class NavigationXamarin : ContentPage
    {
        public async Task PushPopupAsync(PopupPage popupPage )
        {
            await Navigation.PushPopupAsync(popupPage);
        }
        public async Task PopAllPopupAsync()
        {
            await Navigation.PopAllPopupAsync();
        }
    }
    
}
