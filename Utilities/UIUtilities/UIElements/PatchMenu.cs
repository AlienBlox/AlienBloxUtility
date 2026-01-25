using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Terraria.GameContent.UI.Elements;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class PatchMenu : UIPanel
    {
        public PatchMenu()
        {
            Width.Set(0, 1f);
            Height.Set(0, 1f);

            ConHostRender.SetModal(true, true, this);

            this.InsertText("Sorry, Patcher Feature is under construction...");
        }
    }
}