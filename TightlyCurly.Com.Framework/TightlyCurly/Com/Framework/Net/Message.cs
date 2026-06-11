namespace TightlyCurly.Com.Framework.Net;

public class Message
{
    private MessageCredentials _credentials = null;

    private MessageFormat _messageFormat = MessageFormat.Html;

    private string _subject = string.Empty;

    private string _body = string.Empty;

    private string _from = string.Empty;

    private string _to = string.Empty;

    private string _cc = string.Empty;

    private string _bcc = string.Empty;

    private string _replyTo = string.Empty;

    public string Subject
    {
        get
        {
            return _subject;
        }
        set
        {
            _subject = value;
        }
    }

    public string Body
    {
        get
        {
            return _body;
        }
        set
        {
            _body = value;
        }
    }

    public string From
    {
        get
        {
            return _from;
        }
        set
        {
            _from = value;
        }
    }

    public string To
    {
        get
        {
            return _to;
        }
        set
        {
            _to = value;
        }
    }

    public string Cc
    {
        get
        {
            return _cc;
        }
        set
        {
            _cc = value;
        }
    }

    public string Bcc
    {
        get
        {
            return _bcc;
        }
        set
        {
            _bcc = value;
        }
    }

    public string ReplyTo
    {
        get
        {
            return _replyTo;
        }
        set
        {
            _replyTo = value;
        }
    }

    public MessageFormat MessageFormat
    {
        get
        {
            return _messageFormat;
        }
        set
        {
            _messageFormat = value;
        }
    }

    public MessageCredentials Credentials
    {
        get
        {
            return _credentials;
        }
        set
        {
            _credentials = value;
        }
    }

    public Message()
    {
    }

    public Message(MessageCredentials credentials)
    {
        _credentials = credentials;
    }

    public Message(string to, string from, string subject, string body, MessageCredentials credentials)
        : this(credentials)
    {
        _to = to;
        _from = from;
        _subject = subject;
        _body = body;
    }

    public Message(string to, string from, string subject, string body, MessageFormat MessageFormat, MessageCredentials credentials)
        : this(to, from, subject, body, credentials)
    {
        _messageFormat = MessageFormat;
    }
}
