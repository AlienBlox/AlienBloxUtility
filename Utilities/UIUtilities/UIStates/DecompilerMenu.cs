using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DecompilerMenu : UIState
    {
        public DraggableUIWrapper panel;

        private bool Fixer = false;

        public override void OnInitialize()
        {
            panel = new(new Vector2(300, 500), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.DecompUI").Value, true);

            Append(panel);
        }

        public void OnClick(UIMouseEvent evt, UIElement element)
        {
            DebugUtilityList.DecompilerMenuEnabled = false;

            ModContent.GetInstance<DebugPanelStackRender>().Element.buttons[2].Toggle = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fixer)
            {
                panel.Close.OnLeftClick += OnClick;

                Fixer = true;
            }

            base.Update(gameTime);
        }
    }
}