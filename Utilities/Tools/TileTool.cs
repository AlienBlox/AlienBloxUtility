using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.DataStructures;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.UI;

namespace AlienBloxUtility.Utilities.Tools
{
    public class TileTool : DebugTool
    {
        public override void OnToolUse(Player user, bool sudo)
        {
            if (Main.myPlayer == user.whoAmI)
            {
                try
                {
                    AdvancedTileData Tdata = new((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
                    var Tile = Tdata.AssociatedTile;

                    UICommon.TooltipMouseText(Language.GetText("Mods.AlienBloxUtility.UI.TileInfoHover").Format
                    (
                        Tdata.HasAssociatedEntity,
                        Tdata.HasAssociatedEntity == true ? Tdata.AssociatedTE.type : "Nil",
                        ContentIDToString.TileToString(Tile.TileType),
                        $"{Tile.TileFrameX}, {Tile.TileFrameY}",
                        ContentIDToString.WallToString(Tile.WallType),
                        Enum.GetName(Tile.Slope)
                    ));
                }
                catch
                {

                }
            }
        }
    }
}