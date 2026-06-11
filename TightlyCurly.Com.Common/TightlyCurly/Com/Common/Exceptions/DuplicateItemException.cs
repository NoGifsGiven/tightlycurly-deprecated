using System;

namespace TightlyCurly.Com.Common.Exceptions;

[Serializable]
public class DuplicateItemException : Exception
{
    public DuplicateItemException()
    {
    }

    public DuplicateItemException(string message)
        : base(message)
    {
    }

    public DuplicateItemException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
