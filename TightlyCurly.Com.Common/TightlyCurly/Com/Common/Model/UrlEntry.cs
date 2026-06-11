using System;

namespace TightlyCurly.Com.Common.Model;

public class UrlEntry : IModelEntity
{
    public int Id
    {
        get
        {
            return UrlEntryId;
        }
        set
        {
            UrlEntryId = value;
        }
    }

    public int UrlEntryId { get; set; }

    public string Key { get; set; }

    public Uri Uri { get; set; }

    public string Priority { get; set; }

    public ChangeFrequency ChangeFrequency { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public ObjectType ObjectType { get; set; }

    public int ObjectId { get; set; }
}
