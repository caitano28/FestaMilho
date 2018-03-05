using System;
using FestaMilho.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using FestaMilho.View;
using System.Collections.Generic;
using FestaMilho.Services;

namespace FestaMilho.ViewModel
{
    public class MainViewModel
    {
        #region Propriedades
        public LoginViewModel NovoLogin { get; set; }
        public CadastroViewModel NovoCadastro { get; set; }
        private APIService apiService; // inicializa api
        public RecuperarViewModel recuperarViewModel { get; set; }
        public CardapioViewModel cardapioViewModel { get; set; }
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public CardapioViewModel Cardapio {get; set;}
        #endregion
        #region Construtor
        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            NovoLogin = new LoginViewModel();
            apiService = new APIService();
            recuperarViewModel = new RecuperarViewModel();
            cardapioViewModel = new CardapioViewModel();
            NovoCadastro = new CadastroViewModel();
            LoadMenu();
        }
        #endregion
        #region Metodos
        

        
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_shortcut_rank.png",
                PageName = "Rank",
                Title = "Ranking das Barracas"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_shortcut_restaurant.png",
                PageName = "Cardapio",
                Title = "Cardápio"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_shortcut_map.png",
                PageName = "Stand",
                Title = "Mapa da Festa"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_shortcut_sobre.png",
                PageName = "Sobre",
                Title = "Sobre"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_shortcut_person.png",
                PageName = "Sair",
                Title = "Sair"
            });

        }
        #endregion


    }
}
