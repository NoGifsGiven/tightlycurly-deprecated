using System;
using System.Runtime.Serialization;

namespace TightlyCurly.Com.Common.Model;

[DataContract]
public class QuestionCategory
{
    [DataMember(Name = "QuestionCategoryId", Order = 1)]
    public int QuestionCategoryId { get; set; }

    [DataMember(Name = "Category", Order = 2)]
    public string Category { get; set; }

    [DataMember(Name = "EnteredDate", Order = 3)]
    public DateTimeOffset EnteredDate { get; set; }

    [DataMember(Name = "UpdatedDate", Order = 4)]
    public DateTimeOffset UpdatedDate { get; set; }

    [DataMember(Name = "ParentId", Order = 5)]
    public int? ParentId { get; set; }
}
