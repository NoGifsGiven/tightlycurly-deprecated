using System;
using TightlyCurly.Com.Framework.Data.Enums;

namespace TightlyCurly.Com.Framework.Data;

public class DataObjectStateEventArgs : EventArgs
{
    private StateChangedType _stateChangedType = StateChangedType.Unchanged;

    public StateChangedType StateChangedType
    {
        get
        {
            return _stateChangedType;
        }
        set
        {
            if (!_stateChangedType.Equals(value))
            {
                _stateChangedType = value;
            }
        }
    }

    public DataObjectStateEventArgs()
    {
    }

    public DataObjectStateEventArgs(StateChangedType stateChanged)
    {
        _stateChangedType = stateChanged;
    }
}
