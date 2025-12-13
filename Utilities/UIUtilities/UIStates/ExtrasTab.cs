using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ExtrasTab : UIState
    {
        public PanelV2 MainP;

        public override void OnInitialize()
        {
            MainP = new(new(300, 500), Vector2.Zero, new(0, 150, 0), Color.Black, "Extras");

            Append(MainP);
        }
    }
}