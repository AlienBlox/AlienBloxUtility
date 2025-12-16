using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class AboutTab : UIState
    {
        public PanelV2 MainP;

        public bool Fix;

        public override void OnInitialize()
        {
            MainP = new(new(300, 500), Vector2.Zero, new(0, 150, 0), Color.Black, Language.GetText("Mods.AlienBloxUtility.UI.AboutTab").Value, true);

            Append(MainP);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                MainP.Close.OnLeftClick += (_, _) => { AboutPageRender.ShowAboutPage = false; };

                Fix = true;
            }

            base.Update(gameTime);
        }
    }
}