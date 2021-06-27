using System;

namespace Laboratoria.Dto
{
    /// <summary>DTO для передачи данных аккаунта.</summary>
    public class AccDto
    {
        /// <summary>Начальник смены.</summary>
        public UserSmenaDto Nach { get;}

        /// <summary>Лаборант.</summary>
        public UserSmenaDto Lab { get; }

        /// <summary>Название смены.</summary>
        public string NameSmena { get; }

        /// <summary>Начало  смены.</summary>
        public DateTime StartSmena { get;}

        /// <summary>Конец смены.</summary>
        public DateTime EndSmena { get;  }

        public AccDto(UserSmenaDto nach, UserSmenaDto lab, string nameSmena, DateTime startSmena, DateTime endSmena)
        {
            Nach = nach;
            Lab = lab;
            NameSmena = nameSmena;
            StartSmena = startSmena;
            EndSmena = endSmena;
        }
    }
}