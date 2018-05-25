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
using Xamarin.Forms;

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
        public ObservableCollection<MediaAvaliacao> ListRank { get; set; }
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
            ListRank = new ObservableCollection<MediaAvaliacao>();
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
            ValidaToken();
            LoadCardapio();
            LoadBarraca();
            LoadRank();
        }

        #endregion
        #region Metodos
        private async void ValidaToken()
        {
            var user = dataService.GetUser();
            if (user != null && user.LembrarSenha)
            {
                var login = new LoginRequest
                {
                    email = user.email,
                    senha = user.senha
                };
                var response = await apiService.Login(login);
                if (response.IsReLogin)
                {
                    await dialogServices.ShowMessage("Usuário Alterado", response.Message);
                    await navigationServices.Navigate("Sair");
                    return;
                }
                if (response.IsSuccess)
                {
                    var usuario = new Usuario
                    {
                        Id = user.Id,
                        _id = user._id,
                        nivel = response.Usuario.usuario.nivel,
                        email = user.email, //comentar essa linha qd usar api
                        LembrarSenha = true,
                        senha = user.senha,
                        Token = response.Usuario.token,
                        nome = response.Usuario.usuario.nome

                    };
                    App.CurrentUser = usuario;
                    dataService.DeleteUser(usuario);
                    dataService.InsertUser(usuario);
                }
            }
                
        }
        public async void LoadRank() //Carrega o Rank no banco local
        {
            var reponse = await apiService.GetMedia();
            if (reponse.IsSuccess)
            {
                string [] Cor = { "#FF0000","#00FF00","#0026FF","#FBFF00","#FC9B00"} ;
                var rankTable = dataService.GetRank();
                if (rankTable != null)
                {
                    conexao.DropTable<Model.Rank>();
                    conexao.CreateTable<Model.Rank>();
                }
                var Contador = 0;
                foreach (var x in reponse.MediaAvaliacao)
                {     
                    
                    
                    var list = dataService.GetBarracaID(x.barraca);
                    foreach (var y in list)
                    {
                        var rank = new Model.Rank
                        {
                          Nome = y.nome,
                          Nota = (double)x.media,
                          Cor = Cor[Contador]
                        };

                        dataService.InsertRank(rank);
                        Contador++;
                        if (Contador > 4)
                        {
                            Contador = 0;
                        }
                    }
                }

            }

        }
                    
        public async void LoadCardapio() //Carrega os cardapios
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
        } 

        private void InsertCardapioLocal()// insere cardapio no sq lite
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

        public async void LoadBarraca()//carrega lista de barracas
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
        }

        private void InsertBarracaLocal()//insere barracas no sq lite
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
        } 

        public ICommand BuscarCommand { get { return new RelayCommand(BuscaCardapio); } }
        public async void BuscaCardapio()
        {
            var list = dataService.GetCardapios(CardapioFilter);
            ReloadCardapios(list);
            await navigationXamarin.PushPopupAsync(new ListCardapioPop());
        }//caixadebusca


        #region MenuTelaInicial
        public ICommand RankingCommand { get { return new RelayCommand(Ranking); } }
        public ICommand CardapioCommand { get { return new RelayCommand(Cardapio); } }
        public ICommand MapaCommand { get { return new RelayCommand(Mapa); } }
        public async void Ranking()
        {
            await navigationServices.Navigate("Rank");
        }
        public async void Cardapio()
        {
            await navigationServices.Navigate("Cardapio");
        }
        public async void Mapa()
        {
            await navigationServices.Navigate("Stand");
        }
        #endregion

        public ICommand PesquisarBarracaCommand { get { return new RelayCommand(PesquisaBarraca); } }
        public async void PesquisaBarraca()
        {
            await navigationXamarin.PushPopupAsync(new ListBarracaPop());
        }

        public ICommand BuscarBarracaCommand { get { return new RelayCommand(BuscaBarraca); } }
        public async void BuscaBarraca()
        {
            var list = dataService.GetBarracas(BarracaFilter);
            ReloadBarracas(list);
            await navigationXamarin.PushPopupAsync(new ListBarracaPop());
        }//caixadebusca
        public ICommand AvaliarCommand { get { return new RelayCommand(AvaliarBarraca); } }
        public async void AvaliarBarraca()
        {
            DateNow = DateTime.Now;
            if (DateNow.Hour >= 19 )
            {
                AvaliacaoRequest.barraca = CurrentBarraca._id;
                await navigationXamarin.PushPopupAsync(new VotePage());
            }
            else
            {
                await dialogServices.ShowMessage("Aviso", "Você só pode avaliar uma Barraca das 19 Hrs até as 7 da manhã! " + DateNow.ToString());
            }
           
           // 
            
        }
        public ICommand CloseCommand { get { return new RelayCommand(Close); } }
        public async void Close()
        {
            await navigationXamarin.PopAllPopupAsync();
            

        }
        public ICommand MaisPratosCommand { get { return new RelayCommand(MaisPratos); } }
        public async void MaisPratos()
        {
            await navigationXamarin.PushPopupAsync(new ListCardapioBarracaPop());
        }
        public ICommand MaisBarracasCommand { get { return new RelayCommand(MaisBarracas); } }
        public async void MaisBarracas()
        {
            await navigationXamarin.PushPopupAsync(new ListBarracaPop());
        }
        public ICommand MaisBarracasRankCommand { get { return new RelayCommand(MaisBarracasRank); } }
        public async void MaisBarracasRank()
        {
            await navigationXamarin.PushPopupAsync(new RankPop());
        }
        public ICommand Avaliar2Command { get { return new RelayCommand(PostAvaliacao); } }
        public async void PostAvaliacao()
        {
            await navigationXamarin.PushPopupAsync(new LoadingPop());
            var Voto = new Avaliacao
            {
                barraca = CurrentBarraca._id,
                nota = (decimal)AvaliacaoRequest.nota
            };
            var response = await apiService.Votar(Voto);
           
            await navigationXamarin.PopAllPopupAsync();
            if (!response.IsSuccess)
            {
                await dialogServices.ShowMessage("Erro", response.Message);
                AvaliacaoRequest.nota = 0;
                return;
            }
            await dialogServices.ShowMessage("Parabéns!", "Voto Realizado com Sucesso!");
            AvaliacaoRequest.nota = 0;
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
