using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Helpers;

public interface IModelConverter<TParam, TResult> where TResult : class
{
    TResult Convert(TParam value);
}
public interface IModelConverter<TResult> where TResult : class
{
    TResult Convert(IModelEntity value);
}
