using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DebugSidebar : UIState
    {
        public UIPanel Sidebar;

        public UIList SidebarList;

        public ButtonIcon NoclipTool, HitboxTool, BlackHoleTool, ScriptingTool, SlimeGame;

        public bool Fix;

        public override void OnInitialize()
        {
            Sidebar = new()
            {
                Width = new(0, 0)
            };

            SidebarList = Sidebar.InsertList();

            NoclipTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Noclip", ItemID.CreativeWings, Color.MintCream);
            HitboxTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Hitbox", ItemID.Wood, Color.SandyBrown);
            BlackHoleTool = new("Mods.AlienBloxUtility.UI.SidebarTools.BlackHole", ItemID.BlackDye, Color.Black);
            ScriptingTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Scripting", ItemID.Amber, Color.Yellow);
            SlimeGame = new("Mods.AlienBloxUtility.UI.SidebarTools.SlimeGame", ItemID.PinkGel, Color.LightPink);

            NoclipTool.HAlign = HitboxTool.HAlign = BlackHoleTool.HAlign = ScriptingTool.HAlign = SlimeGame.HAlign = .5f; //Noice

            SidebarList.AddRange([NoclipTool, HitboxTool, BlackHoleTool, ScriptingTool, SlimeGame]);

            Sidebar.Append(SidebarList);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                Sidebar.SetPadding(0);
                Sidebar.VAlign = ShowUtilityMenuButton.Instance._UI.VAlign;
                Sidebar.HAlign = ShowUtilityMenuButton.Instance._UI.HAlign;
                Sidebar.Width = ShowUtilityMenuButton.Instance._UI.Width;
                Sidebar.Height.Set(500, 0);
                Sidebar.Top.Set(-70f, 0);
                Sidebar.Left.Set(10f, 0);
                //Sidebar.BackgroundColor = new(150, 0, 0, 128);

                SidebarList.MaxWidth = Sidebar.MaxHeight = new(0, 1);
                SidebarList.Width = Sidebar.MaxWidth;
                SidebarList.Height = Sidebar.MaxHeight;

                Sidebar.Append(SidebarList);
                Append(Sidebar);

                Fix = true;
            }

            base.Update(gameTime);
        }
    }
}