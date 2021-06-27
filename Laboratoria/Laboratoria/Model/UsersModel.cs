#define withoutDB // Переменная конфигураци определяющая сборку для тестовой работы без БД.
using Laboratoria.Contexts;

using Laboratoria.Dto;
using Laboratoria.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratoria.Model
{
    public class UsersModel
    {
        // Для работы без БД создаются тестовые коллекци для имитации таблиц БД.
#if withoutDB
        private IReadOnlyList<string> positions = Array.AsReadOnly(new string[] { "Начальник", "Лаборант" });
        private UserSmenaEntity[] users =
        {
            new UserSmenaEntity() {Id = 1, FIO="Иванов Иван Иваныч"},
            new UserSmenaEntity() {Id = 2, FIO="Федоров Федор Фёдорович"},
            new UserSmenaEntity() {Id = 3, FIO="Сидоров Сидор Сидорович"},
            new UserSmenaEntity() {Id = 4, FIO="Николаев Николай Николаевич"}
        };
        private List<AccDto> accounts = new List<AccDto>();
        private IReadOnlyList<AccDto> roAccounts;
        public UsersModel()
        {
            users[0].Position = positions[0];
            users[1].Position = positions[0];
            users[2].Position = positions[1];
            users[3].Position = positions[1];

            roAccounts = accounts.AsReadOnly();
        }
#endif

        public IEnumerable<UserSmenaDto> GetUsers()
        {
            using (UsersSmenaContext ctx = new UsersSmenaContext())
            {
                // Если сборка без БД, то возврат по тестовым таблицам.
#if withoutDB
                return users.Select(Create).ToList();
#else
                return ctx.UsersSmenas.Select(Create).ToList();
#endif
            }
        }

        public async Task<IEnumerable<UserSmenaDto>> GetUsersAsync()
            => await Task.Run(GetUsers);

        internal static UserSmenaDto Create(UserSmenaEntity user)
            => new UserSmenaDto(user.Id, user.FIO, user.Position);

        public IReadOnlyList<string> GetPositions() => positions;
        public async Task<IReadOnlyList<string>> GetPositionsAsync()
            => await Task.Run(GetPositions);
        public IReadOnlyList<AccDto> GetAccounts() => roAccounts;
        public async Task<IReadOnlyList<AccDto>> GetAccountsAsync()
            => await Task.Run(GetAccounts);

        /// <summary>Создание акка по полученным данным и запись его в БД.</summary>
        /// <param name="nach">Начальник смены.</param>
        /// <param name="lab">Лаборант.</param>
        /// <param name="nameSmena">Название смены.</param>
        /// <param name="startSmena">Начало смены.</param>
        /// <param name="endSmena">Конец смены.</param>
        /// <returns>Если удалось создать акк и добавить его в БД,
        /// то возвращается этот акк. <br/>
        /// Иначе возвращается <see langword="null"/>.</returns>
        public AccDto AddAccount(UserSmenaDto nach, UserSmenaDto lab, string nameSmena, DateTime startSmena, DateTime endSmena)
        {
            AccDto acc = new AccDto(nach, lab, nameSmena, startSmena, endSmena);
            accounts.Add(acc);
            RaiseAddAccountsChanged(acc);
            return acc;
        }

        public async Task<AccDto> AddAccountAsync(UserSmenaDto nach, UserSmenaDto lab, string nameSmena, DateTime startSmena, DateTime endSmena)
            => await Task.Run(() => AddAccount(nach, lab, nameSmena, startSmena, endSmena));


        /// <summary>Событие извещающее об изменении коллекции Accounts.
        /// Это коллекци отражает таблицу акков из БД.</summary>
        public NotifyChainChangedHandler<AccDto> AccountsChanged;

        /// <summary>Вспомогательный метод для создания события
        /// о добавлении одного акка.</summary>
        /// <param name="acc">Добавленный акк.</param>
        protected void RaiseAddAccountsChanged(AccDto acc)
            => AccountsChanged?.Invoke(this, ChainChangedArgs<AccDto>.Add(acc));

        /// <summary>Вспомогательный метод для создания события
        /// о удалении одного акка.</summary>
        /// <param name="acc">Удалённый акк.</param>
        protected void RaiseRemoveAccountsChanged(AccDto acc)
            => AccountsChanged?.Invoke(this, ChainChangedArgs<AccDto>.Remove(acc));

        /// <summary>Вспомогательный метод для создания события
        /// об очистке Accounts.</summary>
        protected void RaiseClearAccountsChanged(AccDto acc)
            => AccountsChanged?.Invoke(this, ChainChangedArgs<AccDto>.ClearArgs);

    }
}
