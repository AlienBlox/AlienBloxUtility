using AlienBloxUtility.Utilities.Core;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class PerfBar : UIState
    {
        public UIPanel PerfBarPanel;

        public UIText StatDisplay;

        public override void OnInitialize()
        {
            StatDisplay = new("");
            PerfBarPanel = new();

            PerfBarPanel.PaddingLeft = 25;
            PerfBarPanel.PaddingRight = 25;
            PerfBarPanel.Width.Set(0, 1f);
            PerfBarPanel.Height.Set(0, .05f);
            PerfBarPanel.BackgroundColor = new(0, 128, 0, 128);
            PerfBarPanel.HAlign = 0f;
            PerfBarPanel.VAlign = .1f;

            StatDisplay.Width.Percent = 1f;
            StatDisplay.Height.Percent = 1f;

            PerfBarPanel.Append(StatDisplay);

            Append(PerfBarPanel);
        }

        public override void Update(GameTime gameTime)
        {
            AlienBloxDiags diags = new AlienBloxDiags();

            StatDisplay.SetText(Language.GetText("Mods.AlienBloxUtility.UI.PerfBar").Format(AlienBloxDiags.RamFull / 1024, AlienBloxDiags.PrivateBytes / 1024, diags.GetCpuUsagePercent(), AlienBloxDiags.GCMem / 1024));

            base.Update(gameTime);
        }
    }
}