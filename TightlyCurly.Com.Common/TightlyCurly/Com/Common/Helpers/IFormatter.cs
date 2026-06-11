namespace TightlyCurly.Com.Common.Helpers;

public interface IFormatter<TParam, TRetVal>
{
    TRetVal Format(TParam value);
}
