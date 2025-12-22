using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class LuaManager : UIState
    {
        public PanelV2 BackingPanel;

        public UIPanel ScriptManagerBacking, InfoPanel, RefreshScripts;

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


            BackingPanel.Append(ScriptManagerBacking);
            ScriptManagerBacking.Append(InfoPanel);
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
    }
}