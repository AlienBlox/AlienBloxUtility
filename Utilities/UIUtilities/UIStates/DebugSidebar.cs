using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.NetCode.Packets;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DebugSidebar : UIState
    {
        public UIPanel Sidebar, ScriptBar;

        public UITextBoxImproved ScriptBox;

        //public UIList SidebarList;

        public ButtonIcon NoclipTool, HitboxTool, BlackHoleTool, ScriptingTool, NPCImmortalityTool, PlayerImmortalityTool, SpawningTool, SlimeGame, SendLua, SendJS;

        public List<ButtonIcon> Buttons;

        public bool Fix;

        public override void OnInitialize()
        {
            Buttons = [];

            ScriptBar = new();

            Sidebar = new()
            {
                Width = new(0, 0)
            };

            SendJS = new("Mods.AlienBloxUtility.UI.SidebarTools.JSSend", ItemID.Book, Color.MintCream);
            SendLua = new("Mods.AlienBloxUtility.UI.SidebarTools.LuaSend", ItemID.MoonCharm, Color.Blue);

            SendLua.Height.Set(0, 1);
            SendLua.Width.Set(0, .05f);
            SendLua.VAlign = .5f;
            SendLua.HAlign = 1f;

            SendJS.Height.Set(0, 1);
            SendJS.Width.Set(0, .05f);
            SendJS.VAlign = .5f;
            SendJS.HAlign = .95f;

            //SidebarList = Sidebar.InsertList();

            ScriptBox = new("Enter a script...");
            ScriptBox.VAlign = ScriptBox.HAlign = .5f;
            ScriptBox.Width.Set(0, 1);
            ScriptBox.Height.Set(0, 1);
            ScriptBox.SetUnfocusKeys(true, true);
            ScriptBox.SetTextMaxLength(160);
            ScriptBox.Append(SendLua); 
            ScriptBox.Append(SendJS);

            ScriptBar.Width.Set(0, 1);
            ScriptBar.Height.Set(0, .05f);
            ScriptBar.VAlign = 0;
            ScriptBar.HAlign = .5f;
            ScriptBar.BackgroundColor.A = 255;

            ScriptBar.Append(ScriptBox);

            NoclipTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Noclip", ItemID.CreativeWings, Color.MintCream, true);
            HitboxTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Hitbox", ItemID.Wood, Color.SandyBrown, true);
            BlackHoleTool = new("Mods.AlienBloxUtility.UI.SidebarTools.BlackHole", ItemID.BlackDye, Color.Black);
            ScriptingTool = new("Mods.AlienBloxUtility.UI.SidebarTools.Scripting", ItemID.Amber, Color.Yellow, true);
            NPCImmortalityTool = new("Mods.AlienBloxUtility.UI.SidebarTools.NPCImmortality", ItemID.SpectreBar, Color.MediumPurple, true);
            PlayerImmortalityTool = new("Mods.AlienBloxUtility.UI.SidebarTools.PlayerImmortality", ItemID.GuideVoodooDoll, Colors.RarityRed, true);
            SpawningTool = new("Mods.AlienBloxUtility.UI.SidebarTools.SpawnEntity", ItemID.SuspiciousLookingEye, Colors.RarityOrange, true);
            SlimeGame = new("Mods.AlienBloxUtility.UI.SidebarTools.SlimeGame", ItemID.PinkGel, Color.LightPink);

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

            SpawningTool.Width.Set(0, 1);
            SpawningTool.Height.Set(60, 0);

            SlimeGame.Width.Set(0, 1);
            SlimeGame.Height.Set(60, 0);

            NoclipTool.HAlign = HitboxTool.HAlign = BlackHoleTool.HAlign = ScriptingTool.HAlign = SlimeGame.HAlign = .5f; //Noice

            //SidebarList.ManualSortMethod = (_) => { };

            //SidebarList.AddRange([NoclipTool, HitboxTool, BlackHoleTool, ScriptingTool, SlimeGame]);

            //Sidebar.Append(SidebarList);

            SendLua.OnLeftClick += LuaSend;
            SendLua.OnRightClick += LuaSendServer;

            SendJS.OnLeftClick += JSSend;
            SendJS.OnRightClick += JSSendServer;

            AddToSidebar(NoclipTool);
            AddToSidebar(HitboxTool);
            AddToSidebar(BlackHoleTool);
            AddToSidebar(ScriptingTool);
            AddToSidebar(NPCImmortalityTool);
            AddToSidebar(PlayerImmortalityTool);
            AddToSidebar(SpawningTool);
            AddToSidebar(SlimeGame);

            NoclipTool.OnLeftClick += WallClip;
            HitboxTool.OnLeftClick += DoHitboxTool;

            BlackHoleTool.OnLeftClick += BlackHoleItem;
            BlackHoleTool.OnMiddleClick += BlackHoleProjectile;
            BlackHoleTool.OnRightClick += BlackHoleNPC;

            ScriptingTool.OnLeftClick += ActivateScriptingMenu;

            SlimeGame.OnLeftClick += SlimeGameToggle;
        }
        
        public void JSSend(UIEvent evt, UIElement elem)
        {
            AlienBloxUtility.RunJavaScript(ScriptBox.Text);
            ScriptBox.SetText(string.Empty);
        }

        public void JSSendServer(UIEvent evt, UIElement elem)
        {
            AlienBloxUtility.JSServer(ScriptBox.Text);
            ScriptBox.SetText(string.Empty);
        }

        public void LuaSend(UIEvent evt, UIElement elem)
        {
            AlienBloxUtility.Lua(ScriptBox.Text);
            ScriptBox.SetText(string.Empty);
        }

        public void LuaSendServer(UIEvent evt, UIElement elem)
        {
            AlienBloxUtility.LuaServer(ScriptBox.Text);
            ScriptBox.SetText(string.Empty);
        }

        public void ActivateScriptingMenu(UIEvent evt, UIElement elem)
        {
            if (ScriptBar.Parent == null)
            {
                Append(ScriptBar);
            }
            else
            {
                ScriptBar.Remove();
            }
        }

        public static void BlackHoleItem(UIEvent evt, UIElement elem)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                foreach (var item in Main.ActiveItems)
                {
                    item.position = Main.LocalPlayer.position;
                }
            }
            else
            {
                BlackHolePacket.SendBlackHole(0, Main.LocalPlayer.position);
            }
        }

        public static void BlackHoleProjectile(UIEvent evt, UIElement elem)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                foreach (var proj in Main.ActiveProjectiles)
                {
                    proj.position = Main.LocalPlayer.position;
                }
            }
            else
            {
                BlackHolePacket.SendBlackHole(1, Main.LocalPlayer.position);
            } 
        }

        public static void BlackHoleNPC(UIEvent evt, UIElement elem)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                foreach (var npc in Main.ActiveNPCs)
                {
                    npc.position = Main.LocalPlayer.position;
                }
            }
            else
            {
                BlackHolePacket.SendBlackHole(2, Main.LocalPlayer.position);
            }   
        }

        public static void DoHitboxTool(UIEvent evt, UIElement elem)
        {
            if (elem is ButtonIcon icon)
            {
                Main.LocalPlayer.AlienBloxUtility().HitboxTool = icon.Toggle;
            }
        }

        public static void SlimeGameToggle(UIEvent evt, UIElement elem)
        {
            DebugUtilityList.ConsoleWindowEnabled = true;

            ConHostRender.SetModal(true, true, new SlimeRainGame());
        }

        public static void WallClip(UIEvent evt, UIElement elem)
        {
            if (elem is ButtonIcon icon)
            {
                Main.LocalPlayer.AlienBloxUtility().noClipHack = icon.Toggle;

                if (!Main.LocalPlayer.AlienBloxUtility().noClipHack)
                {
                    AlienBloxUtility.SendNoclipHack(Main.LocalPlayer.position, false);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                Sidebar.SetPadding(0);
                Sidebar.VAlign = ShowUtilityMenuButton.Instance._UI.VAlign;
                Sidebar.HAlign = ShowUtilityMenuButton.Instance._UI.HAlign;
                Sidebar.Width = ShowUtilityMenuButton.Instance._UI.Width;
                Sidebar.Height.Set(60 * Sidebar.Children.Count(), 0);
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