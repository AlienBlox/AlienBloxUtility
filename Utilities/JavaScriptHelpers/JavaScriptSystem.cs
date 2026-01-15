using AlienBloxUtility.Utilities.Core;
using Terraria;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.JavaScriptHelpers
{
    public class JavaScriptSystem : ModSystem
    {
        public override void Load()
        {
            var JavaScriptSpawnNPC = (string name) =>
            {
                if (int.TryParse(name, out var i))
                {
                    AlienBloxUtility.SpawnNPCClient(i, (int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y);
                }
                else
                {
                    AlienBloxUtility.SpawnNPCClient(StringToContentID.NPCFromString(name), (int)Main.LocalPlayer.position.X, (int)Main.LocalPlayer.position.Y);
                }
            };

            var JavaScriptKillNPC = (string name) =>
            {
                if (int.TryParse(name, out var i))
                {
                    foreach (var npc in Main.ActiveNPCs)
                    {
                        npc.RequestKill();
                    }
                }
                else
                {
                    foreach (var npc in Main.ActiveNPCs)
                    {
                        npc.RequestKill();
                    }
                }
            };

            AlienBloxUtility.SetValue("Terraria.SpawnNPC", JavaScriptSpawnNPC);
            AlienBloxUtility.SetValue("Terraria.KillNPC", JavaScriptKillNPC);
        }
    }
}