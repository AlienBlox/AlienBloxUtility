using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class Scare : UIState
    {
        public override void OnInitialize()
        {
            HorrificScream scream = new();

            Append(scream);
        }
    }
}