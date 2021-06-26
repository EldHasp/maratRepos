namespace Laboratoria.Dto
{
    /// <summary>DTO для передачи данных работника смены.</summary>
    public class UserSmenaDto
    {
        /// <summary>Уникальный идентификатор.</summary>
        public int Id { get;}

        /// <summary>Фамилия, имя, отчество.</summary>
        public string Fio { get; }

        /// <summary>Должность.</summary>
        public string Position { get; }

        public UserSmenaDto(int id, string fio, string position)
        {
            Id = id;
            Fio = fio;
            Position = position;
        }
    }

}
