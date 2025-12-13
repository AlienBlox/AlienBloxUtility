using System;
using Terraria.Localization;

namespace AlienBloxUtility.Common.Exceptions
{
    public class CommandConflictException : Exception
    {
        public override string Message => "Conflict detected, can't make command";//Language.GetText("Mods.AlienBloxUtility.Exceptions.CommandConflictException").Value;
    }
}