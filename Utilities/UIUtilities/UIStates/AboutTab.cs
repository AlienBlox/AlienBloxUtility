using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class AboutTab : UIState
    {
        public PanelV2 MainP;

        public UIPanel InfoSubDisplay, GitHub, YouTube, Steam, Reddit, Discord, Roblox;

        public UIText Content, GitHubText, YouTubeText, SteamText, RedditText, DiscordText, RobloxText;

        public bool Fix;

        public override void OnInitialize()
        {
            MainP = new(new(300, 500), Vector2.Zero, new(0, 150, 0), Color.Black, Language.GetText("Mods.AlienBloxUtility.UI.AboutTab").Value, true);
            InfoSubDisplay = new();
            GitHub = new();
            YouTube = new();
            Steam = new();
            Reddit = new();
            Discord = new();
            Roblox = new();
            GitHubText = new("");
            YouTubeText = new("");
            SteamText = new("");
            RedditText = new("");
            DiscordText = new("");
            RobloxText = new("");

            GitHub.Height.Set(0, .5f);
            GitHub.Width.Set(0, .33f);
            YouTube.Height.Set(0, .5f);
            YouTube.Width.Set(0, .33f);
            Steam.Height.Set(0, .5f);
            Steam.Width.Set(0, .33f);
            Reddit.Height.Set(0, .5f);
            Reddit.Width.Set(0, .33f);
            Discord.Width.Set(0, .33f);
            Discord.Height.Set(0, .5f);
            Roblox.Width.Set(0, .33f);
            Roblox.Height.Set(0, .5f);

            GitHub.SetPadding(0);
            YouTube.SetPadding(0);
            Steam.SetPadding(0);
            Reddit.SetPadding(0);
            Discord.SetPadding(0);
            Roblox.SetPadding(0);

            GitHubText.Width.Percent =
            YouTubeText.Width.Percent =
            SteamText.Width.Percent =
            RedditText.Width.Percent =
            DiscordText.Width.Percent =
            RobloxText.Width.Percent =
            GitHubText.Height.Percent =
            YouTubeText.Height.Percent =
            SteamText.Height.Percent =
            RedditText.Height.Percent = 
            RedditText.Height.Percent =
            DiscordText.Height.Percent =
            RobloxText.Height.Percent = 1f;

            GitHub.VAlign = YouTube.VAlign = Steam.VAlign = 0;
            Reddit.VAlign = Discord.VAlign = Roblox.VAlign = 1;

            GitHub.Append(GitHubText);
            YouTube.Append(YouTubeText);
            Steam.Append(SteamText);
            Reddit.Append(RedditText);
            Discord.Append(DiscordText);
            Roblox.Append(RobloxText);

            GitHub.HAlign = Reddit.HAlign = 0f;
            YouTube.HAlign = Discord.HAlign = .5f;
            Steam.HAlign = Roblox.HAlign = 1f;

            InfoSubDisplay.SetPadding(0);
            InfoSubDisplay.Width.Set(0, 1f);
            InfoSubDisplay.Height.Set(0, .2f);
            InfoSubDisplay.VAlign = 1;
            InfoSubDisplay.HAlign = 0.5f;

            InfoSubDisplay.Append(GitHub);
            InfoSubDisplay.Append(YouTube);
            InfoSubDisplay.Append(Steam);
            InfoSubDisplay.Append(Reddit);
            InfoSubDisplay.Append(Discord);
            InfoSubDisplay.Append(Roblox);

            MainP.Append(InfoSubDisplay);
            Append(MainP);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                GitHubText.SetText(Language.GetText("Mods.AlienBloxUtility.SocialMedia.GitHub"));
                YouTubeText.SetText(Language.GetText("Mods.AlienBloxUtility.SocialMedia.YouTube"));
                SteamText.SetText(Language.GetText("Mods.AlienBloxUtility.SocialMedia.Steam"));
                RedditText.SetText(Language.GetText("Mods.AlienBloxUtility.SocialMedia.Reddit"));
                DiscordText.SetText(Language.GetText("Mods.AlienBloxUtility.SocialMedia.Discord"));
                RobloxText.SetText(Language.GetText("Mods.AlienBloxUtility.SocialMedia.Roblox"));

                MainP.Close.OnLeftClick += (_, _) => { AboutPageRender.ShowAboutPage = false; };

                Fix = true;
            }

            base.Update(gameTime);
        }
    }
}