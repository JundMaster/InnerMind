
/// <summary>
/// Interface for combinable items
/// </summary>
/// <typeparam name="T">Scriptable item</typeparam>
public interface ICombinable<T> where T : ScriptableItem
{
    /// <summary>
    /// Compares two items and adds it to the inventory if the operation
    /// is valid
    /// </summary>
    /// <param name="item1">First item to compare</param>
    /// <param name="item2">Second item to compare</param>
    /// <param name="inventory">Inventory</param>
    void CombineItem(T item1, T item2, Inventory inventory);
}
