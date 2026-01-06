using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria;
using Terraria.DataStructures;

namespace AlienBloxUtility.Utilities.Commands
{
    public class SummoningCommand : CommandBase
    {
        public override string CommandName => "summon";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            string Param = Params[0];
            string thingToDo = Params[1];

            if (int.TryParse(thingToDo, out int contentID))
            {
                switch (Param.ToLower())
                {
                    case "item":
                        Main.LocalPlayer.QuickSpawnItem(new EntitySource_Misc("conhost"), contentID);
                        break;
                    case "projectile":
                        Projectile.NewProjectile(new EntitySource_Misc("conhost"), Main.LocalPlayer.position, new(0, 0), contentID, 0, 0);
                        break;
                    case "npc":
                        AlienBloxUtility.SpawnNPCClient(contentID, ((int)Main.LocalPlayer.position.X), ((int)Main.LocalPlayer.position.Y));
                        break;
                    case "buff":
                        Main.LocalPlayer.AddBuff(contentID, 60 * 5);
                        break;
                }
            }
            else
            {
                switch (Param.ToLower())
                {
                    case "item":
                        Main.LocalPlayer.QuickSpawnItem(new EntitySource_Misc("conhost"), StringToContentID.ItemFromString(thingToDo));
                        break;
                    case "projectile":
                        Projectile.NewProjectile(new EntitySource_Misc("conhost"), Main.LocalPlayer.position, new(0, 0), StringToContentID.ProjFromString(thingToDo), 0, 0);
                        break;
                    case "npc":
                        AlienBloxUtility.SpawnNPCClient(StringToContentID.NPCFromString(thingToDo), ((int)Main.LocalPlayer.position.X), ((int)Main.LocalPlayer.position.Y));
                        break;
                    case "buff":
                        Main.LocalPlayer.AddBuff(StringToContentID.BuffFromString(thingToDo), 60 * 5);
                        break;
                }
            }
        }
    }
}