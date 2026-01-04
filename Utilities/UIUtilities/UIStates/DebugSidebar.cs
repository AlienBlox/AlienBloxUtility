using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DebugSidebar : UIState
    {
        public UIPanel Sidebar;

        //public UIList SidebarList;

        public ButtonIcon NoclipTool, HitboxTool, BlackHoleTool, ScriptingTool, NPCImmortalityTool, PlayerImmortalityTool, SlimeGame;

        public List<ButtonIcon> Buttons;

        public bool Fix;

        public override void OnInitialize()
        {
            Buttons = [];

            Sidebar = new()
            {
                Width = new(0, 0)
            };

            //SidebarList = Sidebar.InsertList();

            NoclipTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Noclip", ItemID.CreativeWings, Color.MintCream, true);
            HitboxTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Hitbox", ItemID.Wood, Color.SandyBrown, true);
            BlackHoleTool = new("Mods.AlienBloxUtility.UI.SidebarTools.BlackHole", ItemID.BlackDye, Color.Black, true);
            ScriptingTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Scripting", ItemID.Amber, Color.Yellow, true);
            NPCImmortalityTool = new("Mods.AlienBloxUtility.UI.SidebarTools.NPCImmortality", ItemID.SpectreBar, Color.MediumPurple, true);
            PlayerImmortalityTool = new("Mods.AlienBloxUtility.UI.SidebarTools.PlayerImmortality", ItemID.GuideVoodooDoll, Colors.RarityRed, true);
            SlimeGame = new("Mods.AlienBloxUtility.UI.SidebarTools.SlimeGame", ItemID.PinkGel, Color.LightPink, true);

            NoclipTool.Width.Set(0, 1);
            NoclipTool.Height.Set(60, 0);

            HitboxTool.Width.Set(0, 1);
            HitboxTool.Height.Set(60, 0);

            BlackHoleTool.Width.Set(0, 1);
            BlackHoleTool.Height.Set(60, 0);

            ScriptingTool.Width.Set(0, 1);
            ScriptingTool.Height.Set(60, 0);

            NPCImmortalityTool.Width.Set(0, 1);
            NPCImmortalityTool.Height.Set(60, 0);

            PlayerImmortalityTool.Width.Set(0, 1);
            PlayerImmortalityTool.Height.Set(60, 0);

            SlimeGame.Width.Set(0, 1);
            SlimeGame.Height.Set(60, 0);

            NoclipTool.HAlign = HitboxTool.HAlign = BlackHoleTool.HAlign = ScriptingTool.HAlign = SlimeGame.HAlign = .5f; //Noice

            //SidebarList.ManualSortMethod = (_) => { };

            //SidebarList.AddRange([NoclipTool, HitboxTool, BlackHoleTool, ScriptingTool, SlimeGame]);

            //Sidebar.Append(SidebarList);

            AddToSidebar(NoclipTool);
            AddToSidebar(HitboxTool);
            AddToSidebar(BlackHoleTool);
            AddToSidebar(ScriptingTool);
            AddToSidebar(NPCImmortalityTool);
            AddToSidebar(PlayerImmortalityTool);
            AddToSidebar(SlimeGame);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                Sidebar.SetPadding(0);
                Sidebar.VAlign = ShowUtilityMenuButton.Instance._UI.VAlign;
                Sidebar.HAlign = ShowUtilityMenuButton.Instance._UI.HAlign;
                Sidebar.Width = ShowUtilityMenuButton.Instance._UI.Width;
                Sidebar.Height.Set(60 * 7, 0);
                Sidebar.Top.Set(-70f, 0);
                Sidebar.Left.Set(10f, 0);
                Sidebar.BackgroundColor = new(150, 0, 0, 128);

                //SidebarList.MaxWidth = Sidebar.MaxHeight = new(0, 1);
                //SidebarList.Width = Sidebar.MaxWidth;
                //SidebarList.Height = Sidebar.MaxHeight;

                //Sidebar.Append(SidebarList);
                Append(Sidebar);

                //SidebarList.Recalculate();

                Fix = true;
            }

            base.Update(gameTime);
        }

        public void AddToSidebar(ButtonIcon b)
        {
            b.HAlign = .5f;
            b.Top.Set(Buttons.Count * b.Height.Pixels, 0);

            Buttons.Add(b);
            Sidebar.Append(b);
        }
    }
}