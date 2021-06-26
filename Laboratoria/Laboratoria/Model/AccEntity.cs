using System;

namespace Laboratoria.Entities
{
    /// <summary>Сущность отражающая данные аккаунта.</summary>
    internal class AccEntity
    {
        /// <summary>ФИО начальника смены</summary>
        public UserSmenaEntity Nach { get; set; }

        /// <summary>ФИО лаборантки</summary>
        public UserSmenaEntity Lab { get; set; }

        /// <summary>Номер смены</summary>
        public string NameSmena { get; set; }

        /// <summary>Начало  смены</summary>
        public DateTime StartSmena { get; set; }

        /// <summary>Конец смены</summary>
        public DateTime EndSmena { get; set; }
    }
}