using FestaMilho.Model;
using FestaMilho.Services;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;

namespace FestaMilho.ViewModel
{

    public class CadastroViewModel: INotifyPropertyChanged
    {
        private NavigationServices navigationServices; //servico de navegacao de telas
        private DialogServices dialogServices; //servico de alerta
        private APIService apiService; // inicializa api
        private DataService dataService; //agentedobanco
        public event PropertyChangedEventHandler PropertyChanged;

        #region Propierts
        public string Nome { get; set; }
        public string Email { get; set; }
        public string ConfirmaEmail { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public string LabelAlerta { get; set; }
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
        #endregion



        public CadastroViewModel()
        {
            navigationServices = new NavigationServices(); //nv tela
            dialogServices = new DialogServices(); //iniciando servico de alerta
            apiService = new APIService();
            dataService = new DataService();
        }

        public ICommand CadastrarCommand { get { return new RelayCommand(Cadastrar); } }

        private async void Cadastrar()
        {
            if ((string.IsNullOrEmpty(Nome)) || (string.IsNullOrEmpty(Senha)) || (string.IsNullOrEmpty(Email)))
            {
               await dialogServices.ShowMessage("Erro", "Existem campos em branco!");

                return;
            }
            if (!string.Equals(Email, ConfirmaEmail))
            {
                await dialogServices.ShowMessage("Erro:", "Os e-mails digitados são diferentes!");
                return;
            }
            if (Senha != ConfirmaSenha)
            {
                await dialogServices.ShowMessage("Erro:", "As senhas digitados são diferentes!");
                return;
            }
            var cadastro = new CadastroRequest
            {
                nome = Nome,
                email = Email,
                senha = Senha
            };
            IsRunning = true;
            var response = await apiService.Cadastrar(cadastro);
            IsRunning = false;
            if (response.IsSuccess)
            {
                await dialogServices.ShowMessage("Tudo Certo!", response.Message);
                navigationServices.SetLoginPage();
           
            }
            else
            {
                await dialogServices.ShowMessage("Erro", response.Message);
                return;
            }
            
        }
    }
}
