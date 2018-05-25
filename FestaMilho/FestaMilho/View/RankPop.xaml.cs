using FestaMilho.Services;
using FestaMilho.ViewModel;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FestaMilho.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RankPop : PopupPage
    {
        private DataService dataService;
        public ObservableCollection<Model.Rank> RankList { get; set; }
        private NavigationServices navigationServices;

        public RankPop()
        {
            dataService = new DataService();
            navigationServices = new NavigationServices();
            RankList = new ObservableCollection<Model.Rank>();
            LoadBarraca();
            
            InitializeComponent();
            ListClassificacao.ItemsSource = RankList;

        }

        private void LoadBarraca()
        {
            //await navigationServices.SetLoginPage();
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.LoadRank();
            var RankTable = dataService.GetRankNota();
            RankList.Clear();
            foreach (var x in RankTable)
            {
                var barraca = new Model.Rank()
                {
                    Nome = x.Nome,
                    Cor = x.Cor,
                    Nota = x.Nota
                };
                RankList.Add(barraca);
            }


        }
    }
}