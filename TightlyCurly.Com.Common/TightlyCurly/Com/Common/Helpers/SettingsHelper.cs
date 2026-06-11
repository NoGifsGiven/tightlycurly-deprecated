using System;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Helpers;

public class SettingsHelper : ISettingsHelper
{
    private class DefaultSettings
    {
        public static readonly Setting ContactPageEnabled = new Setting
        {
            Key = "ContactPageEnabled",
            Value = "true"
        };
    }

    protected readonly ISettingsDataProvider _settingsDataProvider;

    public bool ContactPageEnabled
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.ContactPageEnabled);
            if (IsSettingEmpty(setting))
            {
                setting = DefaultSettings.ContactPageEnabled;
            }
            bool.TryParse(setting.Value, out var result);
            return result;
        }
    }

    public int ContactPageThreshold
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.ContactPageThreshold);
            int result = 0;
            return (IsSettingEmpty(setting) || !int.TryParse(setting.Value, out result)) ? int.MaxValue : result;
        }
    }

    public string ContactPageDisabledMessage
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.ContactPageDisabledMessage);
            return IsSettingEmpty(setting) ? string.Empty : setting.Value;
        }
    }

    public string SiteBulletin
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.SiteBulletin);
            return IsSettingEmpty(setting) ? string.Empty : setting.Value;
        }
    }

    public string SiteBulletinCssClass
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.SiteBulletinCssClass);
            return IsSettingEmpty(setting) ? string.Empty : setting.Value;
        }
    }

    public string MailFrom
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.MailFrom);
            return IsSettingEmpty(setting) ? string.Empty : setting.Value;
        }
    }

    public string CommentsMailTo
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.CommentsMailTo);
            return IsSettingEmpty(setting) ? string.Empty : setting.Value;
        }
    }

    public string SmtpServer
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.SmtpServer);
            return IsSettingEmpty(setting) ? string.Empty : setting.Value;
        }
    }

    public string SmtpUsername
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.SmtpUsername);
            return IsSettingEmpty(setting) ? string.Empty : setting.Value;
        }
    }

    public string SmtpPassword
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.SmtpPassword);
            return IsSettingEmpty(setting) ? string.Empty : setting.Value;
        }
    }

    public bool SmtpUseSsl
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.SmtpUseSsl);
            if (IsSettingEmpty(setting))
            {
                return false;
            }
            bool.TryParse(setting.Value, out var result);
            return result;
        }
    }

    public int SmtpServerPort
    {
        get
        {
            Setting setting = GetSetting(Constants.SettingsConstants.SmtpServerPort);
            int result = 0;
            return (!IsSettingEmpty(setting) && int.TryParse(setting.Value, out result)) ? result : 0;
        }
    }

    public SettingsHelper(ISettingsDataProvider settingsDataProvider)
    {
        if (settingsDataProvider == null)
        {
            throw new ArgumentNullException("settingsDataProvider");
        }
        _settingsDataProvider = settingsDataProvider;
    }

    private bool IsSettingEmpty(Setting setting)
    {
        return setting == null || setting.SettingId == 0;
    }

    protected Setting GetSetting(string settingName)
    {
        return _settingsDataProvider.GetSettingByName(settingName);
    }
}
