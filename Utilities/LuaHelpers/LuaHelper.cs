using AlienBloxUtility.Utilities.Core;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.LuaHelpers
{
    public class LuaHelper : ModSystem
    {
        public Action<string> InjectJS = (js) => AlienBloxUtility.RunJavaScript(js);

        public Action<string> DecompMod = (decomp) => TModInspector.DecompileModThreadSafe(decomp);

        public Func<Mod[]> GetMods = () => { return ModLoader.Mods; };

        public Action KillBosses = () =>
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                AlienBloxUtility.LuaServer("DestroyBosses()");
            }
            else
            {
                foreach (var item in Main.npc)
                {
                    if (item.boss)
                    {
                        item.StrikeInstantKill();
                    }
                }
            }
        };

        public override void OnModLoad()
        {
            Action<int>myAction = (type) =>
            {
                AlienBloxUtility.SpawnNPCClient(type, (int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y);
            };

            AlienBloxUtility.RegisterFunc("SpawnNPC", myAction);
            AlienBloxUtility.RegisterFunc("JavaScript", InjectJS);
            AlienBloxUtility.RegisterFunc("DestroyBosses", KillBosses);
            AlienBloxUtility.RegisterFunc("decompile", DecompMod);
            AlienBloxUtility.RegisterFunc("getMods", GetMods);
            AlienBloxUtility.LuaEnv.Add(nameof(LuaHelper), this);
            AlienBloxUtility.LuaEnv.Add(nameof(LuaGambling.RNG), LuaGambling.RNG);
        }
    }
}