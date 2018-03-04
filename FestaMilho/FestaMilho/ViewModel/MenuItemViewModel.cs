using FestaMilho.Services;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace FestaMilho.Model
{
    public class MenuItemViewModel
    {
        private NavigationServices navigationServices;
        public MenuItemViewModel()
        {
            navigationServices = new NavigationServices();
        }
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } } //MvvmLightLibs nuget

        private async void Navigate()
        {
            await navigationServices.Navigate(PageName);
        } 
        #endregion
    }
}
