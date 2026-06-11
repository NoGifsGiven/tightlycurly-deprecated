using System;

namespace TightlyCurly.Com.Common.Model;

public interface IModelEntity
{
    DateTimeOffset EnteredDate { get; set; }

    DateTimeOffset UpdatedDate { get; set; }
}
