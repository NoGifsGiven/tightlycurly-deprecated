using System;

namespace TightlyCurly.Com.Common;

public class BindingInfo
{
    private string _dataValueField = string.Empty;

    private string _dataTextField = string.Empty;

    public string DataValueField
    {
        get
        {
            if (string.IsNullOrEmpty(_dataValueField))
            {
                throw new InvalidOperationException("dataValueField is empty");
            }
            return _dataValueField;
        }
        set
        {
            _dataValueField = value;
        }
    }

    public string DataTextField
    {
        get
        {
            if (string.IsNullOrEmpty(_dataTextField))
            {
                throw new InvalidOperationException("dataTextField is empty");
            }
            return _dataTextField;
        }
        set
        {
            _dataTextField = value;
        }
    }
}
