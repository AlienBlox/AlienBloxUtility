using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ModSpecificUtilities : UIState
    {
        public PanelV2 DebugPanel;

        public bool Fix;

        public override void OnInitialize()
        {
            DebugPanel = new(new(300, 500), Vector2.Zero, new(0, 150, 0), Color.Black, "Mods.AlienBloxUtility.UI.ModSpecificUtilities", true);

            Append(DebugPanel);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                DebugPanel.Close.OnLeftClick += (_, _) =>
                {
                    ModSpecificUtilitiesRender.UtilityEnabled = !ModSpecificUtilitiesRender.UtilityEnabled;
                };

                Fix = true;
            }

            base.Update(gameTime);
        }
    }
}