using Laboratoria.Models;
using Laboratoria.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoria.ViewModels
{
    class MainViewModel : ObservableObject
    {
        // загружаем данные
        public MainViewModel()
        {

            Task.Run(() => Smena_Fill());
        }

        private ObservableCollection<AccauntViewModel> _AccauntViewModel;
        public ObservableCollection<AccauntViewModel> AccauntViewModel
        {
            get { return _AccauntViewModel; }
            set
            {
                _AccauntViewModel = value;
                RaisePropertyChangedEvent("AccauntViewModel");
            }
        }

        private ObservableCollection<AccModel> _AccModel;
        public ObservableCollection<AccModel> AccModel
        {
            get { return _AccModel; }
            set
            {
                _AccModel = value;
                RaisePropertyChangedEvent("AccModel");
            }
        }

        private void Smena_Fill()
        {
           
        }

        //Первая монета
        private string FIO_nach;
        public string FIO_Nach
        {
            get
            {
                Smena_Fill();
                return FIO_nach;
            }
            set
            {
                FIO_nach = value;
                RaisePropertyChangedEvent("FIO_Nach");
            }
        }
    }

  
}
