using System;

namespace TightlyCurly.Com.Common.Model;

public class Setting
{
    public int SettingId { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }
}
