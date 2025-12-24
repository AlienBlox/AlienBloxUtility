using System;
using Terraria;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Lua
{
    public class LuaHelper : ModSystem
    {
        public override void OnModLoad()
        {
            Action<int>myAction = (type) =>
            {
                AlienBloxUtility.SpawnNPCClient(type, (int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y);
            };

            AlienBloxUtility.RegisterFunc("SpawnNPC", myAction);
            AlienBloxUtility.LuaEnv.Add(nameof(LuaHelper), this);
            AlienBloxUtility.LuaEnv.Add(nameof(LuaGambling.RNG), LuaGambling.RNG);
        }
    }
}