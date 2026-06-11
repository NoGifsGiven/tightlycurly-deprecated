using TightlyCurly.Com.Framework.Net;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public interface IEmailProvider
{
    void SendMail(Message message);
}
