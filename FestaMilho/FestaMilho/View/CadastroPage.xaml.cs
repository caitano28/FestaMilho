using FestaMilho.Model;
using FestaMilho.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FestaMilho.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastroPage : ContentPage
	{
        public DialogServices dialogServices;
        public NavigationServices navigationServices;
		public CadastroPage (Usuario usuario)
		{
            navigationServices = new NavigationServices(); //nv tela
            InitializeComponent ();
            TxtEmail.Text = usuario.email;
            TxtSenha.Text = usuario.senha;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
         
            if ((string.IsNullOrEmpty(TxtNome.Text)) || (string.IsNullOrEmpty(TxtSenha.Text)) || (string.IsNullOrEmpty(TxtEmail.Text)))
            {
                TxtAlerta.Text = "Campos com * devem ser preenchidos!";
               
                return;
            }
            TxtAlerta.Text = "";
        }

        private void BtCancelar_Clicked(object sender, EventArgs e)
        {
            navigationServices.SetLoginPage();
        }
    }
}