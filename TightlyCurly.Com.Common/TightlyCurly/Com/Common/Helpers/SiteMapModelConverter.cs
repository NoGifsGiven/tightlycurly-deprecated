using System;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Helpers;

public class SiteMapModelConverter : IModelConverter<UrlEntry, SiteMapEntry>
{
    public SiteMapEntry Convert(UrlEntry value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }
        SiteMapEntry siteMapEntry = new SiteMapEntry();
        siteMapEntry.Uri = value.Uri;
        siteMapEntry.ChangeFrequency = GetChangeFrequency(value.ChangeFrequency);
        siteMapEntry.LastModifiedDate = value.UpdatedDate;
        siteMapEntry.Priority = value.Priority;
        return siteMapEntry;
    }

    private string GetChangeFrequency(ChangeFrequency changeFrequency)
    {
        return changeFrequency switch
        {
            ChangeFrequency.Hourly => "hourly", 
            ChangeFrequency.Daily => "daily", 
            ChangeFrequency.Weekly => "weekly", 
            ChangeFrequency.Monthly => "monthly", 
            ChangeFrequency.Yearly => "yearly", 
            _ => "weekly", 
        };
    }
}
