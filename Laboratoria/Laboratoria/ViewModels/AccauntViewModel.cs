using Laboratoria.Models;
using Laboratoria.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Laboratoria.ViewModels
{
    class AccauntViewModel : ObservableObject, IDataErrorInfo
    {
        //кнопка подтверждения
        public ICommand BtnCommand { get; set; }

        private Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        private ObservableCollection<UsersViewModel> users = new ObservableCollection<UsersViewModel>();
        public ObservableCollection<UsersViewModel> Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisePropertyChangedEvent("Users");
            }
        }

        // загружаем данные
        public AccauntViewModel()
        {
            BtnCommand = new DelegateCommand(Confirmation);
            Task.Run(() => Smena_Fill());         
        }


        // читаем юзеров для комбобокса
        private async Task Smena_Fill()
        {        
            using (UsersSmenaContext ctx = new UsersSmenaContext())
            {
                var q = await (from c in ctx.UsersSmenas select c).ToListAsync();
                foreach (UserSmena userSmena in q)
                {                                 
                        dispatcher.Invoke(() => Users.Add(new UsersViewModel()
                        {
                            Fio = userSmena.FIO,
                            Position = userSmena.Position,                         
                        }));                 
                }
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

        //ФИО начальника смены
        private string FIO_nach;
        public string FIO_Nach
        {
            get
            {
                return FIO_nach;
            }
            set
            {
                FIO_nach = value;
                RaisePropertyChangedEvent("FIO_Nach");
            }
        }
        //ФИО лаборантки
        private string FIO_lab;
        public string FIO_Lab
        {
            get
            {
                return FIO_lab;
            }
            set
            {
                FIO_lab = value;
                RaisePropertyChangedEvent("FIO_Lab");
            }
        }

        List<string> _source_Smena = new List<string> { "Смена №1", "Смена №2", "Смена №3", "Смена №4" };
        public List<string> Source_Smena
        {
            get { return _source_Smena; }
        }
        List<string> _source_TSmena = new List<string> { "08:00-20:00", "20:00-08:00" };
        public List<string> Source_TSmena
        {
            get { return _source_TSmena; }
        }


        //Номер смены
        private string Number_smena;
        public string Number_Smena
        {
            get
            {
                return Number_smena;
            }
            set
            {
                Number_smena = value;
                RaisePropertyChangedEvent("Number_Smena");
            }
        }
        //начало конец смены
        private string Time_smena;
        public string Time_Smena
        {
            get
            {
                return Time_smena;
            }
            set
            {
                Time_smena = value;
                RaisePropertyChangedEvent("Time_Smena");
            }
        }

        //Дата смены
        private DateTime? Date_smena;
        public DateTime? Date_Smena
        {
            get
            {
                return Date_smena;
            }
            set
            {
                Date_smena = value;
                RaisePropertyChangedEvent("Date_Smena");
            }
        }

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
                    if (string.IsNullOrEmpty(FIO_Nach) || FIO_Nach.Length < 6)
                        result = "Выберите ФИО начальника смены";
                }
                if (columnName == "FIO_Lab")
                {
                    if (string.IsNullOrEmpty(FIO_lab) || FIO_lab.Length < 6)
                        result = "Выберите ФИО лаборантки";
                }
                if (columnName == "Number_Smena")
                {
                    if (string.IsNullOrEmpty(Number_Smena) || Number_Smena.Length < 6)
                        result = "Выберите номер смены";
                }
                if (columnName == "Time_Smena")
                {
                    if (string.IsNullOrEmpty(Time_Smena) || Time_Smena.Length < 6)
                        result = "Выберите  начало и конец смены";
                }
                if (columnName == "Date_Smena")
                {
                    if (string.IsNullOrEmpty(Date_Smena.ToString()) || Date_Smena.ToString().Length < 6)
                    { result = "Выберите дату начала смены"; }
                    else 
                    {
                       string s= Convert.ToDateTime(Date_Smena).ToString("dd-MM-yyyy");
                        int comparison;                  
                        comparison = Convert.ToDateTime(Date_Smena).CompareTo(DateTime.Now);             
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

     

        public void Confirmation(object o)
        {
            string start = "";
            string end = "";

            if (Time_Smena == "08:00-20:00")
            {
                DateTime s = Convert.ToDateTime(Date_Smena).Add(new TimeSpan(08, 00, 00));
                start = s.ToString();
                //
                DateTime s1 = Convert.ToDateTime(Date_Smena).Add(new TimeSpan(20, 00, 00));
                end = s1.ToString();
            }
            else if (Time_Smena == "20:00-08:00")
            {
                DateTime s = Convert.ToDateTime(Date_Smena).Add(new TimeSpan(20, 00, 00));
                start = s.ToString();
                //
                DateTime s1 = Convert.ToDateTime(Date_Smena).AddDays(1);
                DateTime s2 = s1.Date.Add(new TimeSpan(08, 00, 00));
                end = s2.ToString();
            }




            string message = "Проверте правильно ли забиты данные?\n";
            message += "Ф.И.О нач. смены: "+ FIO_Nach+ "\n";
            message += "Ф.И.О лаборантки: " + FIO_Lab + "\n";
            message += Number_Smena.ToString() + "\n";
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
