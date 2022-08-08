namespace ServiceP.Helper;
using System.Globalization;

public class AppException : Exception
{
    public AppException() : base() { }

    public AppException(string message) : base(message) { }

    public AppException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}

public class MissingException : Exception
{
    public MissingException() : base() { }

    public MissingException(string message) : base(message) { }

    public MissingException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
