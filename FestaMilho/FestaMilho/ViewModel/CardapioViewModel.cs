using FestaMilho.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FestaMilho.ViewModel
{
    public class CardapioViewModel : INotifyPropertyChanged
    {
        private NavigationServices navigationServices; //servico de navegacao de telas
        private DialogServices dialogServices; //servico de alerta
  
        public CardapioViewModel()
        {
            navigationServices = new NavigationServices();
            dialogServices = new DialogServices();

        }

        public event PropertyChangedEventHandler PropertyChanged;

       
    }
}
