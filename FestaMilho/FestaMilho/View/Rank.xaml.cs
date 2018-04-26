using FestaMilho.Model;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FestaMilho.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rank : ContentPage
    {
        
        public ObservableCollection<Barraca> BarracaList { get; set; }

        public Rank ()
		{

            BarracaList = new ObservableCollection<Barraca>();
            LoadBarraca();
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            List<Microcharts.Entry> entries = new List<Microcharts.Entry>();
            foreach (var x in BarracaList)
            {
                var Entrada = new Microcharts.Entry((float)(x.Avaliacao))
                {
                    Color = SKColor.Parse(x.Cor),
                    ValueLabel = x.Avaliacao.ToString(),
                    Label = x.Nome
                };
                entries.Add(Entrada);
            }
            Grafico.Chart = new Microcharts.BarChart() { Entries = entries };
            Grafico.Chart.LabelTextSize = 40 ;

        }

        private void LoadBarraca()
        {
            BarracaList.Add(new Barraca
            {
                Nome = "Milhosoft",
                Id = 01,
                Localizacao = "Centro",
                Visitantes = 1000,
                Avaliacao = 10,
                Cor = "#FF3A0E"
            });
            BarracaList.Add(new Barraca
            {
                Nome = "DireitoFobia",
                Id = 01,
                Localizacao = "Peliferia",
                Visitantes = 10,
                Avaliacao = 4,
                Cor = "#24DC0E"
            });
        }
    }
}
    