using Laboratoria.Dto;
using Simplified;

namespace Laboratoria.ViewModels
{
    public class UsersViewModel : BaseInpc
    {

        private string _fio;
        public string Fio { get => _fio; set => Set(ref _fio, value); }

        private string _position;
        public string Position { get => _position; set => Set(ref _position, value); }

        private UserSmenaDto _dto;
        public UserSmenaDto Dto { get => _dto; private set => Set(ref _dto, value); }

        public void SetDto(UserSmenaDto dto)
        {
            Dto = dto;
            Fio = dto.Fio;
            Position = dto.Position;
        }

        public UsersViewModel()
        {}

        public UsersViewModel(UserSmenaDto dto)
        {
            SetDto(dto);
        }

        public static UsersViewModel Create(UserSmenaDto dto)
            => new UsersViewModel(dto);
    }
}