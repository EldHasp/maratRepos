using Laboratoria.Dto;
using Laboratoria.Model;
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

        private readonly UsersModel model;

        public AccauntViewModel(UsersModel model)
        {
            this.model = model;
            Positons = model.GetPositions();
            UsersFillAsync();
        }

        public IReadOnlyList<string> Positons { get; }

        private readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        //public ObservableCollection<UsersViewModel> Users { get; }
        //    = new ObservableCollection<UsersViewModel>();
        public ObservableCollection<UserSmenaDto> Users { get; }
            = new ObservableCollection<UserSmenaDto>();

        //// загружаем данные
        //public AccauntViewModel()
        //{
        //    BtnCommand = new RelayCommand(Confirmation);
        //    UsersFillAsync();
        //}


        // читаем юзеров для комбобокса
        private async void UsersFillAsync()
        {
            try
            {
                var users = await model.GetUsersAsync();
                var result = dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (var userSmena in users)
                        Users.Add(userSmena);

                }));
                await result.Task;

            }
            catch (Exception)
            {
                // Здесь вывод об ошибке
            }
        }


        //public ObservableCollection<AccModel> AccModel { get; }
        //    = new ObservableCollection<AccModel>();



        //ФИО начальника смены
        private UserSmenaDto _nach;
        public UserSmenaDto Nach { get => _nach; set => Set(ref _nach, value); }

        //ФИО лаборантки
        private UserSmenaDto _lab;
        public UserSmenaDto Lab { get => _lab; set => Set(ref _lab, value); }

        public IReadOnlyList<string> Smenas { get; }
            = new List<string>
            {
                "Смена №1",
                "Смена №2",
                "Смена №3",
                "Смена №4"
            }
            .AsReadOnly();

        public IReadOnlyList<string> TimesSmenas { get; }
            = new List<string> { "08:00-20:00", "20:00-08:00" }
            .AsReadOnly();


        //Номер смены
        private string _nameSmena;
        public string NameSmena { get => _nameSmena; set => Set(ref _nameSmena, value); }

        //начало конец смены
        private string _timeSmena;
        public string TimeSmena { get => _timeSmena; set => Set(ref _timeSmena, value); }

        //Дата смены
        private DateTime? _dateSmena;
        public DateTime? DateSmena { get => _dateSmena; set => Set(ref _dateSmena, value); }

        public string Error => string.Join(Environment.NewLine,
            errors
            .Where(pair => !string.IsNullOrWhiteSpace(pair.Value))
            .Select(pair => $"{pair.Key}: \"{pair.Value}\""));

        //private int _errors = 0;

        public string this[string columnName]
        {
            get
            {
                string error = null;
                if (columnName == nameof(Nach))
                {
                    if (Nach?.Position != Positons[0] || !Users.Contains(Nach))
                        error = "Выберите ФИО начальника смены";
                }
                else if (columnName == nameof(Lab))
                {
                    if (Lab?.Position != Positons[1] || !Users.Contains(Lab))
                        error = "Выберите ФИО лаборантки";
                }
                else if (columnName == nameof(NameSmena))
                {
                    if (NameSmena == null || !Smenas.Contains(NameSmena))
                        error = "Выберите номер смены";
                }
                else if (columnName == nameof(TimeSmena))
                {
                    if (TimeSmena == null || !TimeSmena.Contains(TimeSmena))
                        error = "Выберите  начало и конец смены";
                }
                else if (columnName == nameof(DateSmena))
                {
                    if (DateSmena == null)
                    {
                        error = "Выберите дату начала смены";
                    }
                    else
                    {
                        if (DateSmena > DateTime.Now)
                        {
                            error = "Дата больше текущей";
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(error))
                {
                    if (errors.ContainsKey(columnName))
                        errors.Remove(columnName);
                }
                else
                    errors[columnName] = error;

                return error;
            }
        }


        private readonly Dictionary<string, string> errors = new Dictionary<string, string>();

        // для подсчета кол-ва не забитых данных для валидации
        public void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
                //_errors++;
            }
            else
            {
                if (!((BindingExpressionBase)e.Error.BindingInError).HasError)
                {
                    ((Control)sender).ToolTip = "";
                    //_errors--;
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
            message += "Ф.И.О нач. смены: " + Nach + "\n";
            message += "Ф.И.О лаборантки: " + Lab + "\n";
            message += NameSmena.ToString() + "\n";
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


        private RelayCommand _addAccountCommand;
        public RelayCommand AddAccountCommand => _addAccountCommand
            ?? (_addAccountCommand = new RelayCommand(AddAccountExecute, AddAccountCanExecute));

        private bool AddAccountCanExecute()
        {
            // Здесь проверка правильности всех данных
            // если данные правильные, то возвращается истина

            return errors.Count == 0;
        }

        private void AddAccountExecute()
        {
            DateTime start = new DateTime(), end = new DateTime(), date = DateSmena.Value;
            if (TimeSmena == "08:00-20:00")
            {
                start = date.Date.AddHours(8);
                end = date.Date.AddHours(20);
            }
            else if (TimeSmena == "20:00-08:00")
            {
                start = date.Date.AddHours(20);
                end = date.Date.AddDays(1).AddHours(8);
            }

            var acc = model.AddAccount(Nach, Lab, NameSmena, start, end);
        }
    }
}
