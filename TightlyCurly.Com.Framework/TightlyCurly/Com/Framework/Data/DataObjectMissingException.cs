using System;

namespace TightlyCurly.Com.Framework.Data;

public class DataObjectMissingException : ApplicationException
{
    public DataObjectMissingException()
    {
    }

    public DataObjectMissingException(string message)
        : base(message)
    {
    }

    public DataObjectMissingException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
