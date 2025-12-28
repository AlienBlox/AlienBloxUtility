using System;

namespace AlienBloxUtility.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public override string Message => $"Item Conflict detected! (In: {StackTrace})";
    }
}