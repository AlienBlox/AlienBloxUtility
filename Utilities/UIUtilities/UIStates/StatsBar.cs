using AlienBloxUtility.Utilities.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class StatsBar : UIState
    {
        public UIPanel StatsBarPanel;

        public UIText StatDisplay;
        public UIText StatDisplaySecondary;

        public override void OnInitialize()
        {
            StatDisplay = new("", 0.5f);
            StatDisplaySecondary = new("", 0.5f);
            StatsBarPanel = new();

            StatsBarPanel.PaddingLeft = 25;
            StatsBarPanel.PaddingRight = 25;
            StatsBarPanel.Width.Set(0, 1f);
            StatsBarPanel.Height.Set(0, .05f);
            StatsBarPanel.BackgroundColor = new(128, 0, 0, 128);
            StatsBarPanel.HAlign = 0f;
            StatsBarPanel.VAlign = .15f;

            StatDisplay.Width.Percent = 1f;
            StatDisplay.Height.Percent = .5f;

            StatDisplaySecondary.Height.Percent = .5f;
            StatDisplaySecondary.Width.Percent = 1f;
            StatDisplaySecondary.VAlign = 1f;

            StatsBarPanel.Append(StatDisplay);
            StatsBarPanel.Append(StatDisplaySecondary);

            Append(StatsBarPanel);
        }

        public override void Update(GameTime gameTime)
        {
            Player Plr = Main.LocalPlayer;

            StatDisplay.SetText(Language.GetText("Mods.AlienBloxUtility.UI.StatsBar.PrimaryStats").Format(
                Plr.GetDamage(Terraria.ModLoader.DamageClass.Melee).Additive,
                Plr.GetDamage(Terraria.ModLoader.DamageClass.Magic).Additive,
                Plr.GetDamage(Terraria.ModLoader.DamageClass.Ranged).Additive,
                Plr.GetDamage(Terraria.ModLoader.DamageClass.Summon).Additive,
                Plr.GetDamage(Terraria.ModLoader.DamageClass.Generic).Additive
            ));

            StatDisplaySecondary.SetText(Language.GetText("Mods.AlienBloxUtility.UI.StatsBar.SecondaryStats").Format(
                Plr.luck,
                Plr.numberOfDeathsPVP,
                Plr.numberOfDeathsPVE
            ));

            base.Update(gameTime);
        }
    }
}