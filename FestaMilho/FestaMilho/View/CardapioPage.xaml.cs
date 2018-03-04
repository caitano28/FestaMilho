using FestaMilho.Model;
using FestaMilho.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FestaMilho.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardapioPage : ContentPage
	{
        public ObservableCollection<Cardapio> NewList { get; set; }
		public CardapioPage ()
		{
           
            NewList = new ObservableCollection<Cardapio>();
            LoadCardapio();
            InitializeComponent ();
		}
        public void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ListCarpio.BeginRefresh();
       
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                ListCarpio.ItemsSource = NewList;
            }
            else
            {
                ListCarpio.ItemsSource = NewList.Where((i => i.Descricao.ToLower().Contains(e.NewTextValue.ToLower()) || (i.Nome.Contains(e.NewTextValue.ToLower()) )));
            }
            ListCarpio.EndRefresh();
        }
        private void LoadCardapio()
        {
            NewList.Add(new Cardapio
            {
                Descricao = "Bolo de Milho",
                Nome = "Bolo",
                FormaPagamento = "Dinheiro",
                Valor = "2.30",
                Id = 1
            });
            NewList.Add(new Cardapio
            {
                Descricao = "Torta de Milho",
                Nome = "Torta",
                FormaPagamento = "Dinheiro",
                Valor = "5.90",
                Id = 1
            });

        }
    }
}