using System.Collections.Generic;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public interface ISettingsDataProvider
{
    Setting GetSettingById(int settingId);

    Setting GetSettingByName(string settingName);

    IEnumerable<Setting> GetAllSettings();

    Setting SaveSetting(int settingId, string key, string value, bool isActive);

    void DeleteSettingById(int settingId);
}
