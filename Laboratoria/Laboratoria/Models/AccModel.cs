
using Simplified;

namespace Laboratoria.Models
{
    public class AccModel : BaseInpc
    {
        //ФИО начальника смены
        private string _fioNach;
        public string FIO_Nach { get => _fioNach; set => Set(ref _fioNach, value); }
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