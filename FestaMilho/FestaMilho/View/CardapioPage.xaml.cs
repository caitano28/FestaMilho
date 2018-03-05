using FestaMilho.Data;
using FestaMilho.Model;
using FestaMilho.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FestaMilho.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardapioPage : ContentPage
    {

        public List<CardapioReturn> CardapioList { get; set; } //loadcardapio
        public List<BarracaReturn> BarracaList { get; set; } //loadbarraca
        private APIService apiService; // inicializa api
        private DataService dataService;
        private Conexao conexao;
        public CardapioPage()
        {
            conexao = new Conexao();
            CardapioList = new List<CardapioReturn>();
            BarracaList = new List<BarracaReturn>();
            apiService = new APIService();
            dataService = new DataService();
            InitializeComponent();
            LoadCardapio();
            LoadBarraca();

        }

       

        public void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ListCarpio.BeginRefresh();
            ListBarraca.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                ListCarpio.ItemsSource = CardapioList;
                ListBarraca.ItemsSource = BarracaList;
            }
            else
            {
                ListCarpio.ItemsSource = CardapioList.Where((i => i.descricao.ToLower().Contains(e.NewTextValue.ToLower()) || (i.nomeprato.Contains(e.NewTextValue.ToLower()))));
                ListBarraca.ItemsSource = BarracaList.Where((i => i.curso.ToLower().Contains(e.NewTextValue.ToLower()) || (i.nome.Contains(e.NewTextValue.ToLower()))));
            }
            ListCarpio.EndRefresh();
            ListBarraca.EndRefresh();
        }//caixadebusca

        private async void LoadCardapio()
        {
          //  ListCarpio.BeginRefresh();
            var response = await apiService.GetCardapio();
            if (response.IsSuccess)
            {
                CardapioList = response.CardapioResult;
            }
            else
            {
                var cardapios = conexao.GetCardapios();
                foreach (var item in cardapios)
                {
                    CardapioList.Add(item);
                }
                return;
            }
            return;
        }
        private async void LoadBarraca()
        {
            var response = await apiService.GetBarraca();
            if (response.IsSuccess)
            {
                BarracaList = response.BarracaResult;
            }
            else
            {
                var barracas = conexao.GetBarracas();
                foreach (var item in barracas)
                {
                    BarracaList.Add(item);
                }
                return;

            }
            return;
        }
    }
}