using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Jint.Native.Object;
using System;

namespace AlienBloxUtility.Utilities.Commands
{
    public class JavaScriptGet : ModCommandHelper
    {
        public override string CommandName => "getjs";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            ObjectInstance global = AlienBloxUtility.JSEngine.Global;

            // Enumerate property names
            foreach (var property in global.GetOwnProperties())
            {
                Console.WriteLine(property.Key);
            }
        }
    }
}