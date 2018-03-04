using System;
using FestaMilho.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using FestaMilho.View;

namespace FestaMilho.ViewModel
{
    public class MainViewModel
    {
        #region Propriedades
        public LoginViewModel NovoLogin { get; set; }
        public CadastroViewModel NovoCadastro { get; set; }
        public RecuperarViewModel recuperarViewModel { get; set; }
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<Cardapio> CardapioList { get; set; }
        public CardapioViewModel Cardapio {get; set;}
        #endregion
        #region Construtor
        public MainViewModel()
        {
            
            CardapioList = new ObservableCollection<Cardapio>();
            Menu = new ObservableCollection<MenuItemViewModel>();
            NovoLogin = new LoginViewModel();
            recuperarViewModel = new RecuperarViewModel();
            NovoCadastro = new CadastroViewModel();
            Cardapio = new CardapioViewModel();
            LoadMenu();
           
            LoadCardapio();

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
                Title= "Cardápio"
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
        
        private void LoadCardapio()
        {
            CardapioList.Add(new Cardapio {
             Descricao = "Bolo de Milho",
             Nome = "Bolo",
             FormaPagamento = "Dinheiro",
             Valor = "5.90",
             Id = 1
            });
            CardapioList.Add(new Cardapio
            {
                Descricao = "Torta de Milho",
                Nome = "Torta",
                FormaPagamento = "Dinheiro",
                Valor = "5.90",
                Id = 1
            });
            
        }
       
        #endregion


    }
}
