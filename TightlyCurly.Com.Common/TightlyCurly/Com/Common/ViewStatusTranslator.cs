namespace TightlyCurly.Com.Common;

public class ViewStatusTranslator
{
    public static int Translate(ViewStatus viewStatus)
    {
        switch (viewStatus)
        {
            case ViewStatus.None:
            case ViewStatus.NotFound:
                return 404;
            case ViewStatus.NotAuthorized:
                return 403;
            default:
                return 200;
        }
    }
}
