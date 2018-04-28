using FestaMilho.Model;
using FestaMilho.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FestaMilho.ViewModel
{
    public class CardapioItemViewModel : CardapioReturn
    {
        private DataService dataService;
        private NavigationServices navigationServices;
        private NavigationXamarin navigationXamarin;
        public ICommand DetalhesBarracaCommand { get { return new RelayCommand(Detalhes); } } //MvvmLightLibs nuget
        public CardapioItemViewModel()
        {
            navigationXamarin = new NavigationXamarin();
            navigationServices = new NavigationServices();
            dataService = new DataService();
        }
        private async void Detalhes()
        {
            var cardapioItemViewModel = new CardapioItemViewModel
            {
                barraca = barraca,
                descricao = descricao,
                nomeprato = nomeprato,
                valor = valor,
                _id = _id,
                __V = __V
            };
            var Barraca = dataService.GetBarracaID(barraca) ;
            foreach (var x in Barraca)
            {
                var barracaItemViewModel = new BarracaItemViewModel
                {
                    curso = x.curso,
                    formapagamento = x.formapagamento,
                    localizacao = x.localizacao,
                    nome = x.nome,
                    periodo =x.periodo,
                    semestre = x.semestre,
                    _id = x._id
                };
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.SetCurrentCardapio(cardapioItemViewModel);
                mainViewModel.SetCurrentBarraca(barracaItemViewModel);
            }

            await navigationXamarin.PopAllPopupAsync();
            await navigationServices.Navigate("CardapioDetailPage");
        }
    }
}
