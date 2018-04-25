using FestaMilho.Classes;
using FestaMilho.Model;
using FestaMilho.Services;
using FestaMilho.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace FestaMilho.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private NavigationServices navigationServices; //servico de navegacao de telas
        private DialogServices dialogServices; //servico de alerta
        private APIService apiService; // inicializa api
        private DataService dataService; //agentedobanco

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoginCommand { get { return new RelayCommand(Login); } } //MvvmLightLibs nuget comando btlogin
        public ICommand CadastrarCommand { get { return new RelayCommand(Cadastar); } }
        public ICommand RecuperarCommand { get { return new RelayCommand(Recuperar); } }

        

        public LoginViewModel()
        {
            navigationServices = new NavigationServices(); //nv tela
            dialogServices = new DialogServices(); //iniciando servico de alerta
            Remenbered = true; // lembrar senha marcado
            apiService = new APIService();
            dataService = new DataService();
        }
        
        #region Properties
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Remenbered { get; set; }
        private bool isRunning;
        public bool IsRunning {
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



        private async void Login()
        {
            var validaEmail = new RegexClass();
            if (!validaEmail.ValidarEmail(Email))
            {
                await dialogServices.ShowMessage("Erro", "E-mail! digitado invalido!");
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                await dialogServices.ShowMessage("Erro", "Preencha o Campo E-mail!");
                return;
            }
            if (string.IsNullOrEmpty(Senha))
            {
                await dialogServices.ShowMessage("Erro", "Preencha o Campo Senha!");
                return;
            }
            //API
            // IsRunning = true;
            await navigationServices.SetLoadingPage();
            var login = new LoginRequest
            {
                email = Email,
                senha = Senha,
            };
            var response = await apiService.Login(login);
            await navigationServices.PopPage();
            //IsRunning = false;
            if (!response.IsSuccess)
            {
                await dialogServices.ShowMessage("Erro", response.Message);
                return;
            }
            else
            {
                var usuario = new Usuario
                {
                    email = Email, //comentar essa linha qd usar api
                    LembrarSenha = Remenbered,
                    senha = Senha,
                    Token = response.Result.ToString(),
                }; //passar da api colocar (Usuario)response.Result;
                dataService.InsertUser(usuario);
                App.Current.MainPage = new MasterPage();
            }

            
        }

        private async void Cadastar()
        {
            var usuario = new Usuario
            {
                email = Email, 
                senha = Senha
            };

           await navigationServices.SetCadastroPage(usuario);
        }
        private async void Recuperar()
        {
            await navigationServices.SetRecuperarPage();
        }
    }
}
