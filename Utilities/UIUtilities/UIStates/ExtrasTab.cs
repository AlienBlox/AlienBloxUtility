using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ExtrasTab : UIState
    {
        public PanelV2 MainP;
        public bool Fix = false;

        public override void OnInitialize()
        {
            MainP = new(new(300, 500), Vector2.Zero, new(0, 150, 0), Color.Black, "Extras");

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