using System;

namespace TightlyCurly.Com.Framework.Data;

public class DataObjectException : ApplicationException
{
    public DataObjectException()
    {
    }

    public DataObjectException(string message)
        : base(message)
    {
    }

    public DataObjectException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
