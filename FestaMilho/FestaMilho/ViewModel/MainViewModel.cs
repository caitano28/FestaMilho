using System;
using FestaMilho.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using FestaMilho.View;
using System.Collections.Generic;
using FestaMilho.Services;
using FestaMilho.Data;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace FestaMilho.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Propriedades
        public LoginViewModel NovoLogin { get; set; }
        public CadastroViewModel NovoCadastro { get; set; }
        private APIService apiService; // inicializa api
        private DataService dataService; //sqlite
        private Conexao conexao;//sqlite
        public RecuperarViewModel recuperarViewModel { get; set; }
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public ObservableCollection<CardapioItemViewModel> Cardapios { get; set; }

         private bool _refreshList;
        public bool RefreshList
        {
            set
            {
                if (_refreshList != value)
                {
                    _refreshList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RefreshList"));
                }
            }
            get
            {
                return _refreshList;
            }
        }
        public string _cardapioFilter { get; set; }
        public string CardapioFilter
        {
            set
            {
                var oldcardapios = Cardapios;
                
                if (_cardapioFilter != value)
                {
                    _cardapioFilter = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CardapioFilter"));
                }
                if (string.IsNullOrEmpty(CardapioFilter))
                {
                    var list = dataService.GetCardapios(CardapioFilter);
                    ReloadCardapios(list);
                }

            }
            get
            {
                return _cardapioFilter;
            }
        }

        #endregion
        #region Construtor
        public MainViewModel()
        {
            instance = this;
            Menu = new ObservableCollection<MenuItemViewModel>();
            Cardapios = new ObservableCollection<CardapioItemViewModel>();
            NovoLogin = new LoginViewModel();
            apiService = new APIService();
            dataService = new DataService();
            conexao = new Conexao();
            recuperarViewModel = new RecuperarViewModel();
            NovoCadastro = new CadastroViewModel();
            LoadMenu();
            LoadCardapio();
        }


        #endregion
        #region Metodos

        private async void LoadCardapio()
        {
            var response = await apiService.GetCardapio();
            if (response.IsSuccess)
            {
                Cardapios.Clear();
                ReloadCardapios(response.CardapioResult.OrderBy(c=> c.nomeprato).ToList());//carrega da api
                var cardapio = dataService.GetCardapio();// verifica se tem cardapio no banco local
                if (cardapio != null)// verifica se tem cardapio no banco local
                {
                    conexao.DropTable<CardapioReturn>();//caso tenha dropa tabela e recarregaga do back
                    conexao.CreateTable<CardapioReturn>();
                    foreach (var x in Cardapios)
                    {
                        var c = new CardapioReturn
                        {
                            barraca = x.barraca,
                            descricao = x.descricao,
                            nomeprato = x.nomeprato,
                            valor = x.valor,
                            _id = x._id,
                            __V = x.__V
                        };
                        var insert = dataService.InsertCardapio(c);
                    }
                }
                else//se náo tiver ja adiciona no banco local
                {
                    foreach (var x in Cardapios)
                    {
                        var c = new CardapioReturn
                        {
                            barraca = x.barraca,
                            descricao = x.descricao,
                            nomeprato = x.nomeprato,
                            valor = x.valor,
                            _id = x._id,
                            __V = x.__V
                        };
                        var insert = dataService.InsertCardapio(c);
                    }
                }
            }// caso nao tiver conexao os dados sao carregados a partir do banco local
            else
            {
                var cardapiolocal = conexao.GetCardapios().ToList();
                ReloadCardapios(cardapiolocal);
                return;
            }

            return;
        } // carrega lista de cardapios
        public ICommand BuscarCommand { get { return new RelayCommand(BuscaCardapio); } }
        public void BuscaCardapio()
        {
            var list = dataService.GetCardapios(CardapioFilter);
            ReloadCardapios(list);
        }//caixadebusca
        private void ReloadCardapios(List<CardapioReturn> L)
        {
            Cardapios.Clear();
            foreach (var x in L)
            {
                var prato = new CardapioItemViewModel
                {
                    nomeprato = x.nomeprato,
                    barraca = x.barraca,
                    descricao = x.descricao,
                    valor = x.valor,
                    _id = x._id,
                    __V = x.__V
                };
                Cardapios.Add(prato);
            }
        }
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
        #region Singleton
        private static MainViewModel instance;

        public event PropertyChangedEventHandler PropertyChanged;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

    }
}
