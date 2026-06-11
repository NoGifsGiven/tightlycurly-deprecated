using System;
using System.Runtime.Serialization;

namespace TightlyCurly.Com.Common.Model;

[DataContract]
public class SiteMapEntry
{
    [DataMember]
    public Uri Uri { get; set; }

    [DataMember]
    public DateTimeOffset LastModifiedDate { get; set; }

    [DataMember]
    public string ChangeFrequency { get; set; }

    [DataMember]
    public string Priority { get; set; }
}
