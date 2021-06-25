using Laboratoria.Models;
using Simplified;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Laboratoria.ViewModels
{
    class MainViewModel : BaseInpc
    {
        private readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        // загружаем данные
        public MainViewModel()
        {

            SmenaFillAsync();
        }

        public ObservableCollection<AccauntViewModel> Accaunts { get; }
            = new ObservableCollection<AccauntViewModel>();

        public ObservableCollection<AccModel> AccModel { get; }
            = new ObservableCollection<AccModel>();

        private async void SmenaFillAsync()
        {
            try
            {
                var accaunts = await Task.Run(GetAccaunts);
                var result = dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (var acc in accaunts)
                        Accaunts.Add(acc);

                }));
                await result.Task;

            }
            catch (Exception ex)
            {
                // Здесь вывод об ошибке
            }
        }

        private static IEnumerable<AccauntViewModel> GetAccaunts()
        {
            using (UsersSmenaContext ctx = new UsersSmenaContext())
                return new AccauntViewModel[0]; // Чё-то возвращаем
        }

        //Первая монета
        private string _fioNach;
        public string FioNach { get => _fioNach; set => Set(ref _fioNach, value); }
    }

  
}
