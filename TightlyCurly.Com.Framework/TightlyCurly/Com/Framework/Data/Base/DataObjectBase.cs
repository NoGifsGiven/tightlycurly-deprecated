using TightlyCurly.Com.Framework.Data.Enums;

namespace TightlyCurly.Com.Framework.Data.Base;

public abstract class DataObjectBase
{
    public delegate void StateChangedHandler(object sender, DataObjectStateEventArgs e);

    public delegate void SavedHandler(object sender, DataObjectStateEventArgs e);

    public delegate void DeletedHandler(object sender, DataObjectStateEventArgs e);

    protected bool _isDirty;

    protected int _id;

    protected bool __initialState = true;

    protected bool _exists;

    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            if (value != _id)
            {
                OnChanged();
                _id = value;
            }
        }
    }

    public bool IsDirty
    {
        get
        {
            return _isDirty;
        }
        set
        {
            _isDirty = value;
        }
    }

    public event StateChangedHandler StateChanged;

    public event SavedHandler Saved;

    public event DeletedHandler Deleted;

    protected virtual void Init()
    {
        _isDirty = false;
        __initialState = false;
    }

    protected virtual void OnChanged()
    {
        if (!__initialState)
        {
            _isDirty = true;
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new DataObjectStateEventArgs(StateChangedType.Changed));
            }
        }
    }

    protected virtual void OnSaved()
    {
        if (this.Saved != null)
        {
            this.Saved(this, new DataObjectStateEventArgs(StateChangedType.Saved));
        }
    }

    protected virtual void OnDeleted()
    {
        if (this.Deleted != null)
        {
            this.Deleted(this, new DataObjectStateEventArgs(StateChangedType.Deleted));
        }
    }
}
