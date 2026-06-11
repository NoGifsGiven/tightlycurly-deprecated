namespace TightlyCurly.Com.Framework.Net;

public class MessageCredentials
{
    private bool _requiresSsl = false;

    private string _server = string.Empty;

    private string _username = string.Empty;

    private string _password = string.Empty;

    private int _port = 0;

    public string Server
    {
        get
        {
            return _server;
        }
        set
        {
            _server = value;
        }
    }

    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
        }
    }

    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            _password = value;
        }
    }

    public int Port
    {
        get
        {
            return _port;
        }
        set
        {
            _port = value;
        }
    }

    public bool RequiresSsl
    {
        get
        {
            return _requiresSsl;
        }
        set
        {
            _requiresSsl = value;
        }
    }

    public MessageCredentials()
    {
    }

    public MessageCredentials(string server, string username, string password)
    {
        _server = server;
        _username = username;
        _password = password;
    }

    public MessageCredentials(string server, string username, string password, int port, bool requiresSsl)
        : this(server, username, password)
    {
        _port = port;
        _requiresSsl = requiresSsl;
    }
}
