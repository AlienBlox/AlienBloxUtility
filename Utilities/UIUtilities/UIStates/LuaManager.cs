using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class LuaManager : UIState
    {
        public PanelV2 BackingPanel;

        public UIPanel ScriptManagerBacking, InfoPanel, RefreshScripts;

        public UIElement BackerElement;

        public UIScrollbar Scrollbar;

        public UIList BackingList;

        public bool Fix;

        public override void OnInitialize()
        {
            BackingPanel = new(new(500, 400), Vector2.Zero, new(0, 150, 0), Color.Black, "Mods.AlienBloxUtility.UI.LuaDisplay", true, minWidth: 500, minHeight: 400);
            BackingList = [];
            ScriptManagerBacking = new();
            InfoPanel = new();
            RefreshScripts = new();
            Scrollbar = new();
            BackerElement = new();

            BackerElement.Width.Set(0, 1);
            BackerElement.Height.Set(-30, 1);
            BackerElement.HAlign = .5f;
            BackerElement.VAlign = 1;
            BackerElement.SetPadding(15);

            ScriptManagerBacking.Width.Set(0, .6f);
            ScriptManagerBacking.Height.Set(0, 1f);
            ScriptManagerBacking.VAlign = 0.5f;

            Scrollbar.OnScrollWheel += HotbarScrollFix;

            BackingList.VAlign = 0.5f;
            BackingList.HAlign = 0.5f;
            BackingList.Height.Set(0, 1);
            BackingList.Width = BackingList.Height;
            BackingList.ManualSortMethod = (_) => { };
            BackingList.Append(Scrollbar);
            BackingList.SetScrollbar(Scrollbar);

            InfoPanel.Width.Set(0, .4f);
            InfoPanel.Height.Set(0, 1f);
            InfoPanel.VAlign = 0.5f;
            InfoPanel.HAlign = 1f;

            ScriptManagerBacking.Append(BackingList);
            BackerElement.Append(ScriptManagerBacking);
            BackerElement.Append(InfoPanel);
            BackingPanel.Append(BackerElement); 
            Append(BackingPanel);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                BackingPanel.Close.OnLeftClick += (_, _) =>
                {
                    LuaManagerRender.LuaManagerEnabled = !LuaManagerRender.LuaManagerEnabled;
                };

                Fix = true;
            }

            base.Update(gameTime);
        }

        public static void HotbarScrollFix(UIScrollWheelEvent evt, UIElement listeningElement) => Main.LocalPlayer.ScrollHotbar(PlayerInput.ScrollWheelDelta / 120);
    }
}