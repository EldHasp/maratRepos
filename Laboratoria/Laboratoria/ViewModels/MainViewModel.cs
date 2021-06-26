using Laboratoria.Contexts;
using Laboratoria.Dto;
using Laboratoria.Model;
using Simplified;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Laboratoria.ViewModels
{
    public class MainViewModel : BaseInpc
    {
        private readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        public UsersModel Model { get; }

        // загружаем данные
        public MainViewModel(UsersModel model)
        {
            Model = model;
            //SmenaFillAsync();
            foreach (var acc in model.GetAccounts())
            {
                Accounts.Add(acc);
            }

            model.AccountsChanged += OnAccountsChanged;
        }

        private async void FillDataAsync()
        {
            try
            {
                IReadOnlyList<AccDto> accounts = await Model.GetAccountsAsync();
                var result = dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (var acc in accounts)
                        Accounts.Add(acc);

                }));
                await result.Task;
            }
            catch (Exception ex)
            {
                // Здесь действия на случай исключения
                throw;
            }
        }

        private void OnAccountsChanged(object sender, ChainChangedArgs<AccDto> args)
        {
            switch (args.Action)
            {
                case NotifyChainChangedAction.Clear:
                    Accounts.Clear();
                    break;
                case NotifyChainChangedAction.Add:
                    foreach (var acc in args.Items)
                    {
                        Accounts.Add(acc);
                    }
                    break;
                case NotifyChainChangedAction.Remove:
                    foreach (var acc in args.Items)
                    {
                        Accounts.Remove(acc);
                    }
                    break;
                default:
                    break;
            }
        }

        // Не понял для чего эта коллекция ?? 
        //public ObservableCollection<AccauntViewModel> Accounts { get; }
        //    = new ObservableCollection<AccauntViewModel>();

        public ObservableCollection<AccDto> Accounts { get; }
            = new ObservableCollection<AccDto>();

        //private async void SmenaFillAsync()
        //{
        //    try
        //    {
        //        var accaunts = await Task.Run(GetAccaunts);
        //        var result = dispatcher.BeginInvoke(new Action(() =>
        //        {
        //            foreach (var acc in accaunts)
        //                Accounts.Add(acc);

        //        }));
        //        await result.Task;

        //    }
        //    catch (Exception)
        //    {
        //        // Здесь вывод об ошибке
        //    }
        //}

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
