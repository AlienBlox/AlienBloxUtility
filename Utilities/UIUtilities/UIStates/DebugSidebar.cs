using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public FixedUIScrollbar Scrollbar;

        public ButtonIcon NoclipTool, HitboxTool, BlackHoleTool, ScriptingTool, SlimeGame;

        public bool Fix;

        public override void OnInitialize()
        {
            NoclipTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Noclip", ItemID.CreativeWings, Color.MintCream);
            HitboxTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Hitbox", ItemID.Wood, Color.SandyBrown);
            BlackHoleTool = new("Mods.AlienBloxUtility.UI.SidebarTools.BlackHole", ItemID.BlackDye, Color.Black);
            ScriptingTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Scripting", ItemID.Amber, Color.Yellow);
            SlimeGame = new("Mods.AlienBloxUtility.UI.SidebarTools.SlimeGame", ItemID.PinkGel, Color.LightPink);

            NoclipTool.HAlign = HitboxTool.HAlign = BlackHoleTool.HAlign = ScriptingTool.HAlign = SlimeGame.HAlign = .5f; //Noice

            SidebarList = [];
            Scrollbar = new(UserInterface.ActiveInstance);

            Scrollbar.Height.Set(0, 1);

            SidebarList.VAlign = SidebarList.HAlign = .5f;
            SidebarList.Height.Set(0, 1);
            SidebarList.Width.Set(0, 1);
            SidebarList.SetScrollbar(Scrollbar); 
            SidebarList.Append(Scrollbar);

            SidebarList.ManualSortMethod = (_) =>
            {

            };

            SidebarList.AddRange([NoclipTool, HitboxTool, BlackHoleTool, ScriptingTool, SlimeGame]);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                Sidebar = new()
                {
                    Width = ShowUtilityMenuButton.Instance._UI.Width
                };

                Sidebar.SetPadding(0);
                Sidebar.VAlign = ShowUtilityMenuButton.Instance._UI.VAlign;
                Sidebar.HAlign = ShowUtilityMenuButton.Instance._UI.HAlign;
                Sidebar.Height.Set(500, 0);
                Sidebar.Top.Set(-70f, 0);
                Sidebar.Left.Set(10f, 0);
                //Sidebar.BackgroundColor = new(150, 0, 0, 128);

                Sidebar.Append(SidebarList);
                Append(Sidebar);

                Fix = true;
            }

            base.Update(gameTime);
        }
    }
}