using System;

namespace TightlyCurly.Com.Common;

public class ItemSelectedEventArgs : EventArgs
{
    public string SelectedItem { get; private set; }

    public ItemSelectedEventArgs(string selectedItem)
    {
        if (string.IsNullOrEmpty(selectedItem))
        {
            throw new ArgumentNullException("selectedItem");
        }
        SelectedItem = selectedItem;
    }
}
