namespace Laboratoria.Model
{

    /// <summary>Делегат события извещающего об изменении коллекции.</summary>
    /// <typeparam name="T">Тип элементов коллекции.</typeparam>
    /// <param name="sender">Источник события.</param>
    /// <param name="args">Аргумент события.</param>
    public delegate void NotifyChainChangedHandler<T>(object sender, ChainChangedArgs<T> args);
}
