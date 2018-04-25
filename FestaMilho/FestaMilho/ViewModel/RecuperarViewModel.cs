using FestaMilho.Classes;
using FestaMilho.Model;
using FestaMilho.Services;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;

namespace FestaMilho.ViewModel
{
    public class RecuperarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private NavigationServices navigationServices; //servico de navegacao de telas
        private DialogServices dialogServices; //servico de alerta
        private APIService apiService; // inicializa api

        public string Email { get; set; }
        private bool isRunning;
        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }


        public RecuperarViewModel()
        {
            navigationServices = new NavigationServices(); //nv tela
            dialogServices = new DialogServices(); //iniciando servico de alerta
            apiService = new APIService();
        }

        public ICommand RecuperarCommand { get { return new RelayCommand(Recuperar); } }

        private async void Recuperar()
        {
            var validaEmail = new RegexClass();
            if (!validaEmail.ValidarEmail(Email))
            {
                await dialogServices.ShowMessage("Erro", "E-mail! digitado invalido!");
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                await dialogServices.ShowMessage("Erro", "E-mail não digitado");
                return;
            }
           // IsRunning = true;
            await navigationServices.SetLoadingPage();
            var recuperar = new RecuperarRequest
            {
                email = Email
            };
            var response = await apiService.Recuperar(recuperar);
            // IsRunning = false;
            await navigationServices.PopPage();
            if (!response.IsSuccess)
            {
                await dialogServices.ShowMessage("Erro", response.Message);
                return;
            }
            else
            {
                await dialogServices.ShowMessage("Tudo Certo!", response.Message);
                await navigationServices.PopPage();
               // navigationServices.SetLoginPage();
            }
        }
    }
}
