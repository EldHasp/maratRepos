using Simplified;

namespace Laboratoria.ViewModels
{
    public class UsersViewModel : BaseInpc
    {

        private string _fio;
        public string Fio { get => _fio; set => Set(ref _fio, value); }
        private string _position;
        public string Position { get => _position; set => Set(ref _position, value); }
    }
}