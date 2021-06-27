#define withoutDB // Переменная конфигураци определяющая сборку для тестовой работы без БД.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laboratoria.Model
{
    /// <summary>Аргумент для события колекции с элементами типа <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">Тип элементов коллекции.</typeparam>
    public class ChainChangedArgs<T> 
    {
        public NotifyChainChangedAction Action { get; }
        public IReadOnlyList<T> Items { get; }

        protected ChainChangedArgs(NotifyChainChangedAction action, IReadOnlyList<T> items)
        {
            Action = action;
            Items = items;
        }

        /// <summary>Создаёт аргумент для события добавления одного элемента в коллекцию.</summary>
        /// <param name="item">Добавленный элемент.</param>
        /// <returns>Возвращает аргумент для события.</returns>
        public static ChainChangedArgs<T> Add(T item)
            => new ChainChangedArgs<T>(NotifyChainChangedAction.Add, Array.AsReadOnly(new T[] { item }));

        /// <summary>Создаёт аргумент для события добавления нескольких элементов в коллекцию.</summary>
        /// <param name="item">Добавленные элементы.</param>
        /// <returns>Возвращает аргумент для события.</returns>
        public static ChainChangedArgs<T> Add(IEnumerable<T> items)
            => new ChainChangedArgs<T>(NotifyChainChangedAction.Add, Array.AsReadOnly(items.ToArray()));

        /// <summary>Создаёт аргумент для события удаления одного элемента из коллекции.</summary>
        /// <param name="item">Удалённый элемент.</param>
        /// <returns>Возвращает аргумент для события.</returns>
        public static ChainChangedArgs<T> Remove(T item)
            => new ChainChangedArgs<T>(NotifyChainChangedAction.Remove, Array.AsReadOnly(new T[] { item }));

        /// <summary>Создаёт аргумент для события удаления нескольких элементов из коллекции.</summary>
        /// <param name="item">Удалённые элементы.</param>
        /// <returns>Возвращает аргумент для события.</returns>
        public static ChainChangedArgs<T> Remove(IEnumerable<T> items)
            => new ChainChangedArgs<T>(NotifyChainChangedAction.Remove, Array.AsReadOnly(items.ToArray()));

        /// <summary>Событие очистки коллекции.</summary>
         /// <returns>Возвращает аргумент для события.</returns>
       public static ChainChangedArgs<T> ClearArgs { get; }
            = new ChainChangedArgs<T>(NotifyChainChangedAction.Clear, null);

    }
}
