using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DecompilerMenu : UIState
    {
        public DecompilerModListDisplay[] modListDecomp;

        public PanelV2 panel;

        public UIPanel backingPanel;

        public UIList modList;

        public UIScrollbar scrollBar;

        public UIElement backer;

        private bool Fixer = false;

        public override void OnInitialize()
        {
            panel = new(new Vector2(300, 500), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.DecompUI").Value, true, true, 300, 500);
            scrollBar = new();
            backer = new();

            backer.Width.Set(0, 1);
            backer.Height.Set(0, 1);
            backer.VAlign = 1f;
            backer.HAlign = .5f;

            scrollBar.OnScrollWheel += LuaManager.HotbarScrollFix;

            backingPanel = new UIPanel();
            backingPanel.Width.Set(0, .9f);
            backingPanel.Height.Set(0, .9f);

            backingPanel.VAlign = 0.5f;
            backingPanel.HAlign = 0.5f;
            backingPanel.BackgroundColor = new(0, 128, 0);

            modList = [];

            modList.HAlign = modList.VAlign = 0.5f;

            modList.Width.Set(0, 1);
            modList.Height.Set(0, 1);

            scrollBar.VAlign = 0.5f;

            modList.SetScrollbar(scrollBar);
            modList.Append(scrollBar);

            panel.SetPadding(5);

            backingPanel.Append(modList);

            backer.Append(backingPanel);
            panel.Append(backer);

            modListDecomp = TModInspector.GetAllMods();
            modList.AddRange(modListDecomp);

            Append(panel);
        }

        public static void OnClick(UIMouseEvent evt, UIElement element)
        {
            DebugUtilityList.DecompilerMenuEnabled = false;

            ModContent.GetInstance<DebugPanelStackRender>().Element.buttons[2].Toggle = false;
        }
        
        public override void Update(GameTime gameTime)
        {
            if (!Fixer)
            {
                panel.Close.OnLeftClick += OnClick;
                backer.MaxHeight.Set(-panel.Topbar.GetDimensions().Height, 1f);

                Fixer = true;
            }

            base.Update(gameTime);
        }
    }
}