using System.Collections.Generic;

namespace TightlyCurly.Com.Framework.Interfaces;

public interface IParser
{
    object Parse(Dictionary<string, object> values);
}
