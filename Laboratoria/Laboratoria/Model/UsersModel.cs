#define withoutDB
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

        public AccDto AddAccount(UserSmenaDto nach, UserSmenaDto lab, string nameSmena, DateTime startSmena, DateTime endSmena)
        {
            AccDto acc = new AccDto(nach, lab, nameSmena, startSmena, endSmena);
            accounts.Add(acc);
            RaiseAddAccountsChanged(acc);
            return acc;
        }

        public async Task<AccDto> AddAccountAsync(UserSmenaDto nach, UserSmenaDto lab, string nameSmena, DateTime startSmena, DateTime endSmena)
            => await Task.Run(() => AddAccount(nach, lab, nameSmena, startSmena, endSmena));



        public NotifyChainChangedHandler<AccDto> AccountsChanged;

        protected void RaiseAddAccountsChanged(AccDto acc)
            => AccountsChanged?.Invoke(this, ChainChangedArgs<AccDto>.Add(acc));
        protected void RaiseRemoveAccountsChanged(AccDto acc)
            => AccountsChanged?.Invoke(this, ChainChangedArgs<AccDto>.Remove(acc));
        protected void RaiseClearAccountsChanged(AccDto acc)
            => AccountsChanged?.Invoke(this, ChainChangedArgs<AccDto>.ClearArgs);

    }

    public enum NotifyChainChangedAction
    {
        Clear,
        Add,
        Remove
    }

    public class ChainChangedArgs<T> 
    {
        public NotifyChainChangedAction Action { get; }
        public IReadOnlyList<T> Items { get; }

        protected ChainChangedArgs(NotifyChainChangedAction action, IReadOnlyList<T> items)
        {
            Action = action;
            Items = items;
        }

        public static ChainChangedArgs<T> Add(T item)
            => new ChainChangedArgs<T>(NotifyChainChangedAction.Add, Array.AsReadOnly(new T[] { item }));
        public static ChainChangedArgs<T> Add(IEnumerable<T> items)
            => new ChainChangedArgs<T>(NotifyChainChangedAction.Add, Array.AsReadOnly(items.ToArray()));
        public static ChainChangedArgs<T> Remove(T item)
            => new ChainChangedArgs<T>(NotifyChainChangedAction.Remove, Array.AsReadOnly(new T[] { item }));
        public static ChainChangedArgs<T> Remove(IEnumerable<T> items)
            => new ChainChangedArgs<T>(NotifyChainChangedAction.Remove, Array.AsReadOnly(items.ToArray()));
        public static ChainChangedArgs<T> ClearArgs { get; }
            = new ChainChangedArgs<T>(NotifyChainChangedAction.Clear, null);

    }

    public delegate void NotifyChainChangedHandler<T>(object sender, ChainChangedArgs<T> args);
}
