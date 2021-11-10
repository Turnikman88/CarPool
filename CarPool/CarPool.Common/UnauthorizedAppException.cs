using System;
using System.Globalization;

namespace CarPool.Common
{
    public class UnauthorizedAppException : ApplicationException
    {
        public UnauthorizedAppException() : base() { }

        public UnauthorizedAppException(string message) : base(message) { }

        public UnauthorizedAppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
