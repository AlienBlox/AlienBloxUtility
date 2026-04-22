using AlienBloxUtility.Utilities.Helpers;
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

        public UIElement Backer;

        public UIText CurMusic;

        public override void OnInitialize()
        {
            MainPanel = new(new(400, 350), new(0, 0), new(200, 150, 0), Color.Black, Language.GetText("Mods.AlienBloxUtility.UI.MusicTool").Value, true, false, 400, 350);

            Nameplate = new()
            {
                VAlign = 1,
                HAlign = .5f,
            };

            Nameplate.Width.Set(0, 1);
            Nameplate.Height.Set(0, .25f);

            Backer = new()
            {
                VAlign = 1,
                HAlign = .5f,
            };

            Backer.Width.Set(0, 1);
            Backer.Height.Set(-30, 1);

            CurMusic = Nameplate.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.MusicPlaying").Value);
            CurMusic.TextOriginY = .5f;

            Backer.Append(Nameplate);

            MainPanel.Append(Backer);
            Append(MainPanel);
        }

        public override void Update(GameTime gameTime)
        {
            CurMusic.SetText(Language.GetText("Mods.AlienBloxUtility.UI.MusicPlaying2").Format(MusicHelper.GetCurrentMusicName()));

            base.Update(gameTime);
        }
    }
}