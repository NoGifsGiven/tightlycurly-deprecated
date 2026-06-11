namespace TightlyCurly.Com.Web;

public interface IUserControl<T>
{
    void DataBindChildren();

    void DataBindControls();

    T GetObject();
}
