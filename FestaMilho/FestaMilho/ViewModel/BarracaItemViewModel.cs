using FestaMilho.Model;
using FestaMilho.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FestaMilho.ViewModel
{
    public class BarracaItemViewModel : BarracaReturn
    {
        private DataService dataService;
        private NavigationServices navigationServices;
        public ICommand DetalhesBarracaCommand { get { return new RelayCommand(Detalhes); } } //MvvmLightLibs nuget


        public BarracaItemViewModel()
        {
            navigationServices = new NavigationServices();
            dataService = new DataService();
        }
        private async void Detalhes()
        {
            var barracaItemViewModel = new BarracaItemViewModel
            {
                curso = curso,
                formapagamento = formapagamento,
                localizacao = localizacao,
                nome = nome,
                periodo = periodo,
                semestre = semestre,
                _id = _id
            };
            var Cardapio = dataService.GetCardapioID(_id);
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.SetCurrentBarraca(barracaItemViewModel);
            mainViewModel.SetCurrentListCardapio(Cardapio);
            await navigationServices.Navigate("BarracaDetailPage");
        }
    }
}
