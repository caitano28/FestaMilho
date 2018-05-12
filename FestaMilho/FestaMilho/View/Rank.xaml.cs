using FestaMilho.Model;
using FestaMilho.Services;
using FestaMilho.ViewModel;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FestaMilho.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rank : ContentPage
    {
        private NavigationServices navigationServices;
        private DataService dataService;
        public ObservableCollection<Model.Rank>RankList { get; set; }
        List<Microcharts.Entry> entries = new List<Microcharts.Entry>();

        public Rank ()
		{
            dataService = new DataService();
            navigationServices = new NavigationServices();
            RankList = new ObservableCollection<Model.Rank>();
            LoadBarraca();
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            
            //foreach (var x in RankList)
            //{
                
                //var Entrada = new Microcharts.Entry((float)(x.Nota))
                //{
                //    Color = SKColor.Parse(x.Cor),
                //    ValueLabel = x.Nota.ToString(),
                //    Label = x.Nome
                //};
                //entries.Add(Entrada);
            //}
            Grafico.Chart = new Microcharts.BarChart() { Entries = entries };
            Grafico.Chart.LabelTextSize = 40 ;
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
            entries.Clear();
            foreach (var x in RankList.Take(3))
            {
                var Entrada = new Microcharts.Entry((float)(x.Nota))
                {
                    Color = SKColor.Parse(x.Cor),
                    ValueLabel = x.Nota.ToString(),
                    Label = x.Nome
                };
                entries.Add(Entrada);
            }
        }
    }
}
    