
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
///  на у даление
/// </summary>
namespace Laboratoria.Class
{
    class Validations : IDataErrorInfo
    {
        public string FIO_Nach { get; set; } //ФИО начальника смены

        public string FIO_Lab { get; set; } //ФИО лаборантки

        public string Number_Smena { get; set; } //номер смены

        public string Time_Smena { get; set; } //начало конец смены

        public string Date_Smena { get; set; } //Дата смены

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FIO_Nach")
                {
                    if (string.IsNullOrEmpty(FIO_Nach) || FIO_Nach.Length < 1)
                        result = "Выберите ФИО начальника смены";
                }
                if (columnName == "FIO_Lab")
                {
                    if (string.IsNullOrEmpty(FIO_Lab) || FIO_Lab.Length < 1)
                        result = "Выберите ФИО лаборантки";
                }

                return result;
            }
        }

    }
}