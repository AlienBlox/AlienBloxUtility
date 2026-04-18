using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class UltimateAudioInterface : UIState
    {
        public PanelV2 MainPanel;

        public UIPanel MusicPlayerBG, Nameplate, DownloadButton;

        public override void OnInitialize()
        {
            MainPanel = new(new(300, 150), new(0, 0), new(200, 150, 0), Color.Black, Language.GetText("Mods.AlienBloxUtility.UI.MusicTool").Value, true, false, 650, 450);

            Append(MainPanel);
        }
    }
}