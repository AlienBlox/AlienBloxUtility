using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ExtrasTab : UIState
    {
        public PanelV2 MainP;
        public UIPanel AboutButton;
        public bool Fix = false;

        public override void OnInitialize()
        {
            AboutButton = new UIPanel();
            MainP = new(new(300, 500), Vector2.Zero, new(0, 150, 0), Color.Black, "Mods.AlienBloxUtility.UI.ExtrasTab", true);

            AboutButton.HAlign = .5f;
            AboutButton.VAlign = 1;
            AboutButton.SetMargin(15);
            AboutButton.Width.Set(0, .9f);
            AboutButton.Height.Set(0, .1f);
            AboutButton.MaxHeight.Set(50, 0);
            AboutButton.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.AboutTab"));
            AboutButton.OnLeftClick += (_, _) => { AboutPageRender.ShowAboutPage = !AboutPageRender.ShowAboutPage; };

            MainP.Append(AboutButton);
            Append(MainP);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                MainP.Close.OnLeftClick += (_, _) => { DebugUtilityList.ExtrasMenuEnabled = false; DebugPanelStackRender.Instance.Element.buttons[5].Toggle = false; };

                Fix = true;
            }

            base.Update(gameTime);
        }
    }
}