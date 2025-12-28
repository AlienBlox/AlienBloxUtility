using System;

namespace AlienBloxUtility.Common.Exceptions
{
    public class DocumentationException : Exception
    {
        public override string Message => "Can't register documentation due to internal name conflict";
    }
}