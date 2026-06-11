using System;
using System.Collections.Generic;
using System.Data.Common;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public class SqlSettingsDataProvider : DataAdapterBase, ISettingsDataProvider
{
    private readonly IConfigurationHelper _configuration;

    public SqlSettingsDataProvider(IConfigurationHelper configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _connectionString = _configuration.DefaultConnectionString;
    }

    public Setting GetSettingById(int settingId)
    {
        if (settingId <= 0)
        {
            throw new ArgumentException("settingId cannot be less than or equal to zero.", "settingId");
        }
        
        var result = new Setting();
        using (DbDataReader dataReader = base.DataAccess.GetSettingByIdAsync(settingId, CancellationToken.None).Result)
        {
            while (dataReader.Read())
            {
                result = LoadSettingFromReader(dataReader);
            }
        }
        return result;
    }

    public Setting GetSettingByName(string settingName)
    {
        try
        {
            if (string.IsNullOrEmpty(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }
            
            var result = new Setting();
            using (DbDataReader dataReader = base.DataAccess.GetSettingByNameAsync(settingName, CancellationToken.None).Result)
            {
                while (dataReader.Read())
                {
                    result = LoadSettingFromReader(dataReader);
                }
            }
            return result;
        }
        catch (Exception)
        {
            // Handle any database exceptions gracefully
            return new Setting();
        }
    }

    public IEnumerable<Setting> GetAllSettings()
    {
        var list = new List<Setting>();
        using (DbDataReader dataReader = base.DataAccess.GetAllSettingsAsync(CancellationToken.None).Result)
        {
            while (dataReader.Read())
            {
                list.Add(LoadSettingFromReader(dataReader));
            }
        }
        return list;
    }

    public Setting SaveSetting(int settingId, string key, string value, bool isActive)
    {
        if (settingId < 0)
        {
            throw new ArgumentException("settingId cannot be negative.", "settingId");
        }
        
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key));
        }
        
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        
        var result = new Setting();
        using (DbDataReader dataReader = base.DataAccess.SaveSettingAsync(settingId, key, value, isActive, CancellationToken.None).Result)
        {
            while (dataReader.Read())
            {
                result = LoadSettingFromReader(dataReader);
            }
        }
        return result;
    }

    public void DeleteSettingById(int settingId)
    {
        if (settingId <= 0)
        {
            throw new ArgumentException("settingId cannot be less than or equal to zero.", "settingId");
        }
        
        base.DataAccess.DeleteSettingByIdAsync(settingId, CancellationToken.None).Wait();
    }

    private Setting LoadSettingFromReader(DbDataReader reader)
    {
        var setting = new Setting();
        setting.SettingId = Convert.ToInt32(reader["SettingId"]);
        setting.Key = reader["Key"]?.ToString() ?? string.Empty;
        setting.Value = reader["Value"]?.ToString() ?? string.Empty;
        
        // Safe boolean parsing with fallback to false
        var isActiveValue = reader["IsActive"];
        if (isActiveValue != DBNull.Value)
        {
            bool.TryParse(isActiveValue.ToString(), out var isActive);
            setting.IsActive = isActive;
        }
        else
        {
            setting.IsActive = false;
        }
        
        // Safe date parsing with fallback to current time
        var enteredDateValue = reader["EnteredDate"]?.ToString();
        if (!string.IsNullOrEmpty(enteredDateValue) && DateTimeOffset.TryParse(enteredDateValue, out var enteredDate))
        {
            setting.EnteredDate = enteredDate;
        }
        
        var updatedDateValue = reader["UpdatedDate"]?.ToString();
        if (!string.IsNullOrEmpty(updatedDateValue) && DateTimeOffset.TryParse(updatedDateValue, out var updatedDate))
        {
            setting.UpdatedDate = updatedDate;
        }
        
        return setting;
    }
}