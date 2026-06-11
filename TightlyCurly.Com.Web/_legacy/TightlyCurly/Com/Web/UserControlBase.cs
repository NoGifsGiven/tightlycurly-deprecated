using System.Web.UI;
using TightlyCurly.Com.Common;

namespace TightlyCurly.Com.Web;

public abstract class UserControlBase<T, U> : UserControl where T : class
{
    private object _view;

    private T _presenter;

    protected T Presenter
    {
        get
        {
            if (_presenter == null)
            {
                _presenter = PresenterFactory.GetPresenter<T, U>(_view);
            }
            return _presenter;
        }
    }

    protected void Initialize(object view)
    {
        _view = view;
    }
}
