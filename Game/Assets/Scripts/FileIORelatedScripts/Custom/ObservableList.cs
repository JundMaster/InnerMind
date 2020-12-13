using System;
using System.Collections.Generic;
using System.Collections.Specialized;

public class ObservableList<T> : List<T>, INotifyCollectionChanged
{
    private bool allowsDuplicates;
    public ObservableList(bool allowsDuplicates = false) : base()
    {
        this.allowsDuplicates = allowsDuplicates;
    }
    public ObservableList(IEnumerable<T> collection, bool allowsDuplicates = false) : base(collection)
    {
        this.allowsDuplicates = allowsDuplicates;
    }
    public ObservableList(int capacity, bool allowsDuplicates = false) : base(capacity)
    {
        this.allowsDuplicates = allowsDuplicates;
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public event Action<ScriptableItem> AddedItem;

    private void OnCollectionChanged(object sender = null,
                                     NotifyCollectionChangedEventArgs e = null)
    {

        CollectionChanged?.Invoke(sender, e);
    }

    private void OnItemAdded(ScriptableItem item) => AddedItem?.Invoke(item);

    public new void Add(T item)
    {
        if (!allowsDuplicates && base.Contains(item))
            return;
            
        base.Add(item);
        OnCollectionChanged();
        OnItemAdded(item as ScriptableItem);
    }
    public new void Remove(T item)
    {
        base.Remove(item);
        OnCollectionChanged();
    }
}