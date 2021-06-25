using Simplified;

namespace Laboratoria.Class
{
    public class DataForRow : BaseInpc
    {
        private string _symbol;
        private decimal _price;
        private ValueDirection _direction;

        public int ID { get; }
        public string Symbol { get => _symbol; set => Set(ref _symbol, value); }
        public decimal Price { get => _price; set => Set(ref _price, value); }
        public ValueDirection Direction { get => _direction; set => Set(ref _direction, value); }

        public DataForRow(int id) => ID = id;

    }
}
