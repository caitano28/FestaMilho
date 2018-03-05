using FestaMilho.Data;
using FestaMilho.Model;
using FestaMilho.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FestaMilho.ViewModel
{
    public class CardapioViewModel : INotifyPropertyChanged
    {
        private NavigationServices navigationServices; //servico de navegacao de telas
        private DialogServices dialogServices; //servico de alerta
        private APIService apiService; // inicializa api
        private DataService dataService;
        private Conexao conexao;
        public List<CardapioReturn> CardapioList { get; set; } //loadcardapio
        public List<BarracaReturn> BarracaList { get; set; } //loadbarraca
        public bool IsRefreshing;
        private bool isRunning;
        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }
        //private string _LabelAlert;
        //public string LabelAlert
        //{
        //    set
        //    {
        //        if (_LabelAlert != value)
        //        {
        //            _LabelAlert = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelAlert"));
        //        }
        //    }
        //    get
        //    {
        //        return _LabelAlert;
        //    }
        //}
        public CardapioViewModel()
        {
            conexao = new Conexao();
            CardapioList = new List<CardapioReturn>();
            BarracaList = new List<BarracaReturn>();
            apiService = new APIService();
            dataService = new DataService();
            navigationServices = new NavigationServices();
            dialogServices = new DialogServices();
            LoadCardapio();
            LoadBarraca();
        }

        private async void LoadBarraca()
        {
            var response = await apiService.GetBarraca();
            if (response.IsSuccess)
            {
                BarracaList = response.BarracaResult;
                var barraca = dataService.GetBarraca();
                if (barraca != null)
                {
                    foreach (var x in BarracaList)
                    {
                        var update = dataService.UpdateBarraca(x);
                    }
                }
                else
                {
                    foreach (var x in BarracaList)
                    {
                        var insert = dataService.InsertBarraca(x);
                    }
                }
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

        private async void LoadCardapio()
        {
            
            var response = await apiService.GetCardapio();
            if (response.IsSuccess)
            {
                CardapioList = response.CardapioResult;
                var cardapio = dataService.GetCardapio();
                if (cardapio != null)
                {
                    foreach (var x in CardapioList)
                    {
                        var update = dataService.UpdateCardapio(x);
                    }
                }
                else
                {
                    foreach (var x in CardapioList)
                    {
                        var insert = dataService.InsertCardapio(x);
                    }
                }
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

        public event PropertyChangedEventHandler PropertyChanged;

       
    }
}
