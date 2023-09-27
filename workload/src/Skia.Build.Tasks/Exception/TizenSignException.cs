using System;
using System.Globalization;

namespace Skia.Build.Tasks
{
    public class SkiaSignException : Exception
    {
        public SkiaSignException() : base()
        {
        }

        public SkiaSignException(string message) : base(message)
        {
        }

        public SkiaSignException(string message, System.Exception inner) : base(message, inner)
        {
        }

        public SkiaSignException(string format, params string[] args)
            : this(string.Format(CultureInfo.CurrentCulture, format, args))
        {
        }
    }
}
