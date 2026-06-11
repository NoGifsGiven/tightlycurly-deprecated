using System;

namespace TightlyCurly.Com.Common;

public class SearchEventArgs : EventArgs
{
    public string SearchItem { get; private set; }

    public SearchEventArgs(string searchItem)
    {
        if (string.IsNullOrEmpty(searchItem))
        {
            throw new ArgumentNullException("searchItem");
        }
        SearchItem = searchItem;
    }
}
