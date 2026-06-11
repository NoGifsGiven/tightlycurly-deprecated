using System;

namespace TightlyCurly.Com.Common.Model;

public abstract class EntityBase : IModelEntity
{
    public int Id { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }
}
