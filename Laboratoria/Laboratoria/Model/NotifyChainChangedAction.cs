#define withoutDB // Переменная конфигураци определяющая сборку для тестовой работы без БД.

namespace Laboratoria.Model
{
    /// <summary>Перечисление для события изменения коллекци.</summary>
    public enum NotifyChainChangedAction
    {
        Clear,
        Add,
        Remove
    }
}
