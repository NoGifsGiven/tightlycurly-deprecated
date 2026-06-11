using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TightlyCurly.Com.Framework.Xml.Serialization;

public class XmlObjectSerializer<T>
{
    public string Serialize(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }
        using MemoryStream memoryStream = new MemoryStream();
        using XmlTextWriter writer = new XmlTextWriter(memoryStream, Encoding.UTF8);
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(writer, value);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        XmlDocument document = new XmlDocument();
        document.Load(memoryStream);
        return document.OuterXml;
    }

    public T Deserialize(string serializedValue)
    {
        if (string.IsNullOrEmpty(serializedValue))
        {
            throw new ArgumentNullException("serializedValue");
        }
        serializedValue = serializedValue.Replace("False", "false");
        serializedValue = serializedValue.Replace("True", "true");
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using StringReader stringReader = new StringReader(serializedValue);
        return (T)serializer.Deserialize(stringReader);
    }
}
