using System;

namespace TightlyCurly.Com.Framework;

public class CollectionChangedEventArgs<T> : EventArgs
{
    private T _item;

    public T Item => _item;

    public CollectionChangedEventArgs(T item)
    {
        _item = item;
    }
}
