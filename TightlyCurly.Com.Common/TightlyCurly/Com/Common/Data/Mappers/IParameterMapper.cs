using System.Collections.Generic;
using System.Data.Common;

namespace TightlyCurly.Com.Common.Data.Mappers;

public interface IParameterMapper
{
    IEnumerable<DbParameter> GetParameters(IEnumerable<NamedParameter> namedParameters);
}
