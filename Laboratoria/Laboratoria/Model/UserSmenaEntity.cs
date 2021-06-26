namespace Laboratoria.Entities
{
    /// <summary>Сущность отражающая работника смены.</summary>
    internal class UserSmenaEntity
    {
        /// <summary>Уникальный идентификатор.</summary>
        public int Id { get; set; }

        /// <summary>Фамилия, имя, отчество.</summary>
        public string FIO { get; set; }

        /// <summary>Должность.</summary>
        public string Position { get; set; }
    }
}
