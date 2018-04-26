using FestaMilho.Data;
using FestaMilho.Model;
using FestaMilho.Services;
using FestaMilho.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace FestaMilho.ViewModel
{
    public class MainViewModel :  INotifyPropertyChanged
    {
        #region Propriedades
        private APIService apiService; // inicializa api
        private DataService dataService; //sqlite
        private Conexao conexao;//sqlite
        private DialogServices dialogServices;
        private NavigationServices navigationServices;
        private NavigationXamarin navigationXamarin;
        public CardapioItemViewModel CurrentCardapio { get; set; }
        public DateTime DateNow { get; set; }
        public AvaliacaoRequest AvaliacaoRequest { get; set; }
        public BarracaItemViewModel CurrentBarraca { get; set; }
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<CardapioItemViewModel> Cardapios { get; set; }
        public ObservableCollection<BarracaItemViewModel> Barracas { get; set; }
        public ObservableCollection<CardapioItemViewModel> Cardapios2 { get; set; }

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
        public string _barracaFilter { get; set; }
        public string BarracaFilter
        {
            set
            {
               

                if (_barracaFilter != value)
                {
                    _barracaFilter = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BarracaFilter"));
                }
                if (string.IsNullOrEmpty(BarracaFilter))
                {
                    var list = dataService.GetBarracas(BarracaFilter);
                    ReloadBarracas(list);
                }

            }
            get
            {
                return _barracaFilter;
            }
        }

      
        #endregion
        #region Construtor
        public MainViewModel()
        {
            instance = this;
            Menu = new ObservableCollection<MenuItemViewModel>();
            Cardapios = new ObservableCollection<CardapioItemViewModel>();
            Cardapios2 = new ObservableCollection<CardapioItemViewModel>();
            Barracas = new ObservableCollection<BarracaItemViewModel>();
            CurrentCardapio = new CardapioItemViewModel();
            CurrentBarraca = new BarracaItemViewModel();
            apiService = new APIService();
            dataService = new DataService();
            AvaliacaoRequest = new AvaliacaoRequest();
            dialogServices = new DialogServices();
            navigationServices = new NavigationServices();
            navigationXamarin = new NavigationXamarin();
            conexao = new Conexao();
            LoadMenu();
            LoadCardapio();
            LoadBarraca();
        }

        


        #endregion
        #region Metodos

        private async void LoadCardapio()
        {
            var response = await apiService.GetCardapio();
            if (response.IsSuccess)
            {
                
                ReloadCardapios(response.CardapioResult.OrderBy(c=> c.nomeprato).ToList());//carrega da api
                var cardapio = dataService.GetCardapio();// verifica se tem cardapio no banco local
                if (cardapio != null)// verifica se tem cardapio no banco local
                {
                    conexao.DropTable<CardapioReturn>();//caso tenha dropa tabela e recarregaga do back
                    conexao.CreateTable<CardapioReturn>();
                
                    InsertCardapioLocal();
                    
                }
                else//se náo tiver ja adiciona no banco local
                {
                    InsertCardapioLocal();
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

        private void InsertCardapioLocal()
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
        } // insere cardapio no sq lite

        private async void LoadBarraca()
        {
            var response = await apiService.GetBarraca();
            if (response.IsSuccess)
            {
                ReloadBarracas(response.BarracaResult.OrderBy(c => c.nome).ToList());//carrega da api
                var barraca = dataService.GetBarraca();// verifica se tem cardapio no banco local
                if (barraca != null)// verifica se tem cardapio no banco local
                {
                    conexao.DropTable<BarracaReturn>();//caso tenha dropa tabela e recarregaga do back
                    conexao.CreateTable<BarracaReturn>();
                    InsertBarracaLocal();
                    
                }
                else//se náo tiver ja adiciona no banco local
                {
                    InsertBarracaLocal();
                }
            }// caso nao tiver conexao os dados sao carregados a partir do banco local
            else
            {
                var barracalocal = conexao.GetBarracas().OrderBy(c=> c.nome).ToList();
                ReloadBarracas(barracalocal);
                return;
            }

            return;
        }//carrega lista de barracas

        private void InsertBarracaLocal()
        {
            foreach (var x in Barracas)
            {
                var c = new BarracaReturn
                {
                    curso = x.curso,
                    formapagamento = x.formapagamento,
                    localizacao = x.localizacao,
                    nome = x.nome,
                    periodo = x.periodo,
                    semestre = x.semestre,
                    _id = x._id,
                    __v = x.__v
                };
                var insert = dataService.InsertBarraca(c);
            }
        } //insere barracas no sq lite

        public ICommand BuscarCommand { get { return new RelayCommand(BuscaCardapio); } }
        public void BuscaCardapio()
        {
            var list = dataService.GetCardapios(CardapioFilter);
            ReloadCardapios(list);
        }//caixadebusca

        public ICommand BuscarBarracaCommand { get { return new RelayCommand(BuscaBarraca); } }
        public void BuscaBarraca()
        {
            var list = dataService.GetBarracas(BarracaFilter);
            ReloadBarracas(list);
        }//caixadebusca
        public ICommand AvaliarCommand { get { return new RelayCommand(AvaliarBarraca); } }
        public async void AvaliarBarraca()
        {
            DateNow = DateTime.Now;
            
            AvaliacaoRequest.barraca = CurrentBarraca._id;
            AvaliacaoRequest.dtvotacao = DateNow.ToString();
            await navigationXamarin.PushPopupAsync(new VotePage());
           // await dialogServices.ShowMessage("Time do Sistema", DateNow.ToString());
            
        }
        public ICommand CloseCommand { get { return new RelayCommand(Close); } }
        public async void Close()
        {
            await navigationXamarin.PopAllPopupAsync();
            

        }
        public ICommand Avaliar2Command { get { return new RelayCommand(PostAvaliacao); } }
        public async void PostAvaliacao()
        {
            await navigationXamarin.PopAllPopupAsync();


        }
        public ICommand WebSiteCommand { get { return new RelayCommand(WebSite); } }
        public async void WebSite()
        {
            await navigationServices.Navigate("WebViewPage");
        }
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
        private void ReloadBarracas(List<BarracaReturn> L)
        {
            Barracas.Clear();
            foreach (var x in L)
            {
                var barraca = new BarracaItemViewModel
                {
                   curso = x.curso,
                   formapagamento = x.formapagamento,
                   localizacao = x.localizacao,
                   nome = x.nome,
                   periodo = x.periodo,
                   semestre = x.semestre,
                   _id = x._id,
                   __v = x.__v
                };
                Barracas.Add(barraca);
            }
        }
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                Icon = "trofeu",
                PageName = "Rank",
                Title = "Ranking das Barracas"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "cardapio",
                PageName = "Cardapio",
                Title = "Cardápio"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "mapa",
                PageName = "Stand",
                Title = "Mapa da Festa"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "sobre",
                PageName = "Sobre",
                Title = "Sobre"
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "sair",
                PageName = "Sair",
                Title = "Sair"
            });

        }
        public void SetCurrentCardapio(CardapioItemViewModel cardapioItem)
        {
            CurrentCardapio = cardapioItem;
        }
        public void SetCurrentBarraca(BarracaItemViewModel barraca)
        {
            CurrentBarraca = barraca;
        }
        public void SetCurrentListCardapio(List<CardapioReturn> cardapio)
        {
            Cardapios2.Clear();
            foreach (var x in cardapio)
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
                Cardapios2.Add(prato);
            }
        }
        #endregion
        #region Singleton
        public static MainViewModel instance;

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
