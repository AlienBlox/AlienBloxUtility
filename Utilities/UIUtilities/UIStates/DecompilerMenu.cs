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

        public DraggableUIWrapper panel;

        public UIPanel backingPanel;

        public UIList modList;

        public UIScrollbar scrollBar;

        private bool Fixer = false;

        public override void OnInitialize()
        {
            panel = new(new Vector2(300, 500), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.DecompUI").Value, true);
            scrollBar = new UIScrollbar();

            backingPanel = new UIPanel();
            backingPanel.Width.Set(0, .9f);
            backingPanel.Height.Set(0, .9f);

            backingPanel.VAlign = 0.6f;
            backingPanel.HAlign = 0.5f;
            backingPanel.BackgroundColor = new(0, 128, 0);

            modList = [];

            modList.HAlign = 0.5f;
            modList.VAlign = 0.5f;

            modList.Width.Set(0, 1);
            modList.Height.Set(0, 1);

            scrollBar.VAlign = 0.5f;

            modList.SetScrollbar(scrollBar);
            modList.Append(scrollBar);

            panel.SetPadding(5);

            backingPanel.Append(modList);

            panel.Append(backingPanel);

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
                modListDecomp = TModInspector.GetAllMods();
                modList.AddRange(modListDecomp);
                panel.Close.OnLeftClick += OnClick;

                Fixer = true;
            }

            base.Update(gameTime);
        }
    }
}