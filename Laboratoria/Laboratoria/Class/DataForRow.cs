using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoria.Class
{
    public class DataForRow : OnPropertyChangedClass
    {
        private string _symbol;
        private decimal _price;
        private ValueDirection _direction;

        public int ID { get; }
        public string Symbol { get => _symbol; set => SetProperty(ref _symbol, value); }
        public decimal Price { get => _price; set => SetProperty(ref _price, value); }
        public ValueDirection Direction { get => _direction; set => SetProperty(ref _direction, value); }

        public DataForRow(int id) => ID = id;

        protected override void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (propertyName == nameof(Price) && newValue is IComparable numb)
            {
                Direction = (ValueDirection)numb.CompareTo(Price);
            }
            base.SetProperty(ref fieldProperty, newValue, propertyName);

        }
    }
}
