using Microsoft.Xna.Framework;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AlienBloxUtility.Utilities.Core
{
    public class AlienBloxScripterInterface : ModSystem
    {
        public static AlienBloxScripterInterface Instance;

        public static ref List<(string, object)> CommonFuncLibraryExposed => ref Instance.CommonFuncLibrary;

        public List<(string, object)> CommonFuncLibrary;

        public override void OnModLoad()
        {
            Instance = this;

            var terrariaTable = new LuaTable();

            terrariaTable["kill"] = (string npcName, string npcMod) =>
            {
                int npcType = 0;

                if (npcMod != "Terraria")
                {
                    npcType = NPCID.Search.GetId(npcName);
                }
                else
                {
                    npcType = NPCID.Search.GetId(npcMod + "/" + npcName);
                }

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    AlienBloxUtility.LuaServer(
                        $@"
                        Terraria.kill({npcType})
                        "
                    );
                }
                else
                {
                    foreach (NPC npc in Main.npc)
                    {
                        if (npc.type == npcType)
                        {
                            npc.StrikeInstantKill();
                        }
                    }
                }
            };
            terrariaTable["spawnItem"] = (string itemName, string itemMod, int stack = 1) =>
            {
                int itemID = 0;

                if (itemMod != "Terraria")
                {
                    itemID = ItemID.Search.GetId(itemName);
                }
                else
                {
                    itemID = ItemID.Search.GetId(itemName + "/" + itemMod);
                }

                Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_Misc("luaSpawn"), itemID, stack);
            };
            terrariaTable["giveBuff"] = (string buffName, string buffMod, int player, int time = 100) =>
            {
                int buffID = 0;

                if (buffMod != "Terraria")
                {
                    buffID = BuffID.Search.GetId(buffName);
                }
                else
                {
                    buffID = BuffID.Search.GetId(buffName + "/" + buffMod);
                }

                Main.player[player].AddBuff(buffID, time);
            };
            terrariaTable["spawnProj"] = (string projName, string projMod, int dmg, int kb) =>
            {
                int projID = 0;

                if (projMod != "Terraria")
                {
                    projID = NPCID.Search.GetId(projName);
                }
                else
                {
                    projID = NPCID.Search.GetId(projName + "/" + projMod);
                }

                Projectile.NewProjectile(new EntitySource_Misc("projLua"), Main.LocalPlayer.position, Vector2.Zero, projID, dmg, kb);
            };
            terrariaTable["spawnNPC"] = (string npcName, string npcMod) =>
            {
                int npcID = 0;

                if (npcMod != "Terraria")
                {
                    npcID = NPCID.Search.GetId(npcName);
                }
                else
                {
                    npcID = NPCID.Search.GetId(npcName + "/" + npcMod);
                }

                AlienBloxUtility.SpawnNPCClient(npcID, ((int)Main.LocalPlayer.position.X), ((int)Main.LocalPlayer.position.Y));
            };

            CommonFuncLibrary = 
            [
                ("Terraria", terrariaTable)
            ];

            foreach (var item in CommonFuncLibraryExposed)
            {
                AlienBloxUtility.LuaEnv[item.Item1] = item.Item2;
            }
        }

        public override void OnModUnload()
        {
            Instance = null;

            CommonFuncLibrary?.Clear();
            CommonFuncLibrary = null;
        }
    }
}