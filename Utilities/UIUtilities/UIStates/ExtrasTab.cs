using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.NetCode;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ExtrasTab : UIState
    {
        public PanelV2 MainP;
        public UIPanel AboutButton, LuaRunner, PacketSuppressor, ModSpecificToggle;
        public bool Fix = false;

        public override void OnInitialize()
        {
            AboutButton = new UIPanel();
            LuaRunner = new UIPanel();
            PacketSuppressor = new UIPanel();
            ModSpecificToggle = new UIPanel();
            MainP = new(new(300, 500), Vector2.Zero, new(0, 150, 0), Color.Black, "Mods.AlienBloxUtility.UI.ExtrasTab", true);

            ModSpecificToggle.HAlign = .5f;
            ModSpecificToggle.VAlign = .7f;
            ModSpecificToggle.SetMargin(15);
            ModSpecificToggle.Width.Set(0, .9f);
            ModSpecificToggle.Height.Set(0, .1f);
            ModSpecificToggle.MaxHeight.Set(50, 0);
            ModSpecificToggle.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.ModSpecificToggle"));
            ModSpecificToggle.Top.Set(-45, 0);
            ModSpecificToggle.OnLeftClick += (_, _) =>
            {
                ModSpecificUtilitiesRender.UtilityEnabled = !ModSpecificUtilitiesRender.UtilityEnabled;
            };

            PacketSuppressor.HAlign = .5f;
            PacketSuppressor.VAlign = .8f;
            PacketSuppressor.SetMargin(15);
            PacketSuppressor.Width.Set(0, .9f);
            PacketSuppressor.Height.Set(0, .1f);
            PacketSuppressor.MaxHeight.Set(50, 0);
            PacketSuppressor.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.PacketSuppressor"));
            PacketSuppressor.Top.Set(-30, 0);
            PacketSuppressor.OnLeftClick += (_, _) =>
            {
                NetSystem.PacketEnabled = !NetSystem.PacketEnabled;

                Main.NewText(Language.GetText("Mods.AlienBloxUtility.Messages.PacketSuppression").Format(!NetSystem.PacketEnabled));
            };

            LuaRunner.HAlign = .5f;
            LuaRunner.VAlign = .9f;
            LuaRunner.SetMargin(15);
            LuaRunner.Width.Set(0, .9f);
            LuaRunner.Height.Set(0, .1f);
            LuaRunner.MaxHeight.Set(50, 0);
            LuaRunner.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.Lua"));
            LuaRunner.Top.Set(-15, 0);
            LuaRunner.OnLeftClick += (_, _) => { LuaManagerRender.LuaManagerEnabled = !LuaManagerRender.LuaManagerEnabled; };

            AboutButton.HAlign = .5f;
            AboutButton.VAlign = 1;
            AboutButton.SetMargin(15);
            AboutButton.Width.Set(0, .9f);
            AboutButton.Height.Set(0, .1f);
            AboutButton.MaxHeight.Set(50, 0);
            AboutButton.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.AboutTab"));
            AboutButton.OnLeftClick += (_, _) => { AboutPageRender.ShowAboutPage = !AboutPageRender.ShowAboutPage; };

            MainP.Append(ModSpecificToggle);
            MainP.Append(LuaRunner);
            MainP.Append(PacketSuppressor);
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