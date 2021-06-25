using Laboratoria.Models;
using Simplified;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace Laboratoria.ViewModels
{
    public class AccauntViewModel : BaseInpc, IDataErrorInfo
    {
        //кнопка подтверждения
        public ICommand BtnCommand { get; set; }

        private readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        public ObservableCollection<UsersViewModel> Users { get; }
            = new ObservableCollection<UsersViewModel>();

        // загружаем данные
        public AccauntViewModel()
        {
            BtnCommand = new RelayCommand(Confirmation);
            SmenaFillAsync();
        }


        // читаем юзеров для комбобокса
        private async void SmenaFillAsync()
        {
            try
            {
                var users = await Task.Run(GetUsersSmenas);
                var result = dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (UsersViewModel userSmena in users)
                        Users.Add(userSmena);

                }));
                await result.Task;

            }
            catch (Exception ex)
            {
                // Здесь вывод об ошибке
            }
        }

        private static UsersViewModel Create(UserSmena userSmena)
            => new UsersViewModel()
            {
                Fio = userSmena.FIO,
                Position = userSmena.Position,
            };

        private static IEnumerable<UsersViewModel> GetUsersSmenas()
        {
            using (UsersSmenaContext ctx = new UsersSmenaContext())
                return ctx.UsersSmenas
                    .ToList()
                    .Select(Create)
                    .ToList();
        }


        public ObservableCollection<AccModel> AccModel { get; }
            = new ObservableCollection<AccModel>();

        //ФИО начальника смены
        private string _fioNach;
        public string FioNach { get => _fioNach; set => Set(ref _fioNach, value); }

        //ФИО лаборантки
        private string _fioLab;
        public string FioLab { get => _fioLab; set => Set(ref _fioLab, value); }

        public IReadOnlyList<string> SourceSmena { get; }
            = new List<string>
            {
                "Смена №1",
                "Смена №2",
                "Смена №3",
                "Смена №4"
            }
            .AsReadOnly();

        public IReadOnlyList<string> SourceTSmena { get; }
            = new List<string> { "08:00-20:00", "20:00-08:00" }
            .AsReadOnly();


        //Номер смены
        private string _numberSmena;
        public string NumberSmena { get => _numberSmena; set => Set(ref _numberSmena, value); }

        //начало конец смены
        private string _timeSmena;
        public string TimeSmena { get => _timeSmena; set => Set(ref _timeSmena, value); }

        //Дата смены
        private DateTime? _dateSmena;
        public DateTime? DateSmena { get => _dateSmena; set => Set(ref _dateSmena, value); }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        private int _errors = 0;

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FIO_Nach")
                {
                    if (string.IsNullOrEmpty(FioNach) || FioNach.Length < 6)
                        result = "Выберите ФИО начальника смены";
                }
                if (columnName == "FIO_Lab")
                {
                    if (string.IsNullOrEmpty(_fioLab) || _fioLab.Length < 6)
                        result = "Выберите ФИО лаборантки";
                }
                if (columnName == "Number_Smena")
                {
                    if (string.IsNullOrEmpty(NumberSmena) || NumberSmena.Length < 6)
                        result = "Выберите номер смены";
                }
                if (columnName == "Time_Smena")
                {
                    if (string.IsNullOrEmpty(TimeSmena) || TimeSmena.Length < 6)
                        result = "Выберите  начало и конец смены";
                }
                if (columnName == "Date_Smena")
                {
                    if (string.IsNullOrEmpty(DateSmena.ToString()) || DateSmena.ToString().Length < 6)
                    { result = "Выберите дату начала смены"; }
                    else
                    {
                        string s = Convert.ToDateTime(DateSmena).ToString("dd-MM-yyyy");
                        int comparison;
                        comparison = Convert.ToDateTime(DateSmena).CompareTo(DateTime.Now);
                        if (comparison > 0)
                        {
                            result = "Дата больше текущей";
                        }
                    }
                }
                return result;
            }
        }

        // для подсчета кол-ва не забитых данных для валидации
        public void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
                _errors++;
            }
            else
            {
                if (!((BindingExpressionBase)e.Error.BindingInError).HasError)
                {
                    ((Control)sender).ToolTip = "";
                    _errors--;
                }

            }
        }



        public void Confirmation()
        {
            string start = "";
            string end = "";

            if (TimeSmena == "08:00-20:00")
            {
                DateTime s = Convert.ToDateTime(DateSmena).Add(new TimeSpan(08, 00, 00));
                start = s.ToString();
                //
                DateTime s1 = Convert.ToDateTime(DateSmena).Add(new TimeSpan(20, 00, 00));
                end = s1.ToString();
            }
            else if (TimeSmena == "20:00-08:00")
            {
                DateTime s = Convert.ToDateTime(DateSmena).Add(new TimeSpan(20, 00, 00));
                start = s.ToString();
                //
                DateTime s1 = Convert.ToDateTime(DateSmena).AddDays(1);
                DateTime s2 = s1.Date.Add(new TimeSpan(08, 00, 00));
                end = s2.ToString();
            }




            string message = "Проверте правильно ли забиты данные?\n";
            message += "Ф.И.О нач. смены: " + FioNach + "\n";
            message += "Ф.И.О лаборантки: " + FioLab + "\n";
            message += NumberSmena.ToString() + "\n";
            message += "Начало смены: " + start + "\n";
            message += "Конец смены: " + end + "\n";
            string caption = "Подтверждение!!!";
            MessageBoxButton buttons = MessageBoxButton.YesNo;


            // Displays the MessageBox.

            var mess = MessageBox.Show(message, caption, buttons,
             MessageBoxImage.Question);
            if (mess == MessageBoxResult.Yes)
            {



            }

        }
    }
}
