
using Laboratoria.MVVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Laboratoria.Models
{
    public class AccModel : ObservableObject
    {
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
        public string FIO_Lab { get; set; }
        //Номер смены
        public string Number_Smena { get; set; }
        //начало  смены
        public string Start_Smena { get; set; }
        //конец смены
        public string End_Smena { get; set; }
    }
}