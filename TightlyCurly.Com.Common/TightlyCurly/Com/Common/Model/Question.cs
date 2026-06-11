using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TightlyCurly.Com.Common.Model;

[DataContract]
[KnownType(typeof(Locale))]
[KnownType(typeof(QuestionCategory))]
public class Question : IModelEntity
{
    [DataMember(Name = "QuestionId", Order = 1)]
    public int QuestionId { get; set; }

    [DataMember(Name = "Question", Order = 2)]
    public string Value { get; set; }

    [DataMember(Name = "Answer", Order = 3)]
    public string Answer { get; set; }

    [DataMember(Name = "Locale", Order = 4)]
    public Locale Locale { get; set; }

    [DataMember(Name = "UpdatedDate", Order = 5)]
    public DateTimeOffset UpdatedDate { get; set; }

    public int Id
    {
        get
        {
            return QuestionId;
        }
        set
        {
            QuestionId = value;
        }
    }

    [DataMember(Name = "EnteredDate", Order = 6)]
    public DateTimeOffset EnteredDate { get; set; }

    [DataMember(Name = "QuestionCategories", Order = 7)]
    public IEnumerable<QuestionCategory> QuestionCategories { get; set; }

    public Question()
    {
        Locale = new Locale();
        QuestionCategories = new List<QuestionCategory>();
    }
}
