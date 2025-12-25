using System;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class JavaScriptHelper : ModSystem
    {
        public Action<string> InjectLua = (lua) => AlienBloxUtility.RunLuaAsync(lua, AlienBloxUtility.GetToken());

        public override void OnModLoad()
        {
            AlienBloxUtility.SetValue("lua", InjectLua);
        }
    }
}