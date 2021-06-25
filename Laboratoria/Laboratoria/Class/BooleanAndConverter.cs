using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Laboratoria.Class
{
    public class BooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool res = false;
            int[] mass= new int[values.Length];
            int count = 0;
            int sum = 0;
            // Пишем в массив
            foreach (object value in values)
            {
                if ((value is bool) && (bool)value == false)
                {
                    mass[count] = 0;
                }
                else
                {
                    mass[count] = 1;
                }
                count++;
            }
            // Считаем сумму
            foreach (int value in mass)
            {
                sum += value;
            }
            if (sum > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

            //    foreach (object value in values)
            //{
            //    if ((value is bool) && (bool)value == false)
            //    {
            //        res= true;
            //    }
            //    else
            //    {
            //        res = false;
            //    }
            //}
            //return res;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }
}
