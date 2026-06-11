using System;
using System.Collections.Generic;
using System.Linq;

namespace TightlyCurly.Com.Common;

public class CollectionConverter<T, U> where T : U
{
    protected List<T> _list;

    public CollectionConverter(IEnumerable<T> values)
    {
        if (values == null)
        {
            throw new ArgumentNullException("values");
        }
        _list = new List<T>();
        _list.AddRange(values);
    }

    public IEnumerable<U> Convert()
    {
        return _list.Select((T o) => (U)(object)o);
    }
}
