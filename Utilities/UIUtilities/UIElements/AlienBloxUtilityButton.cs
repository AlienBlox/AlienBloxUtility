using AlienBloxUtility.Utilities.DataStorage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class AlienBloxUtilityButton : UIElement
    {
        private Texture2D PowerButtonIcon;
        private Texture2D ActivateButtonIcon;
        private Texture2D ActivateButtonOutline;
        private Color DrawColor = Color.White;

        internal bool LoadedSprites = false;

        public AlienBloxUtilityButton()
        {
            ActivateButtonIcon = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/ActivateButton").Value;
            ActivateButtonOutline = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/ActivateButtonOutline").Value;
            PowerButtonIcon = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/PowerButtonIcon").Value;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                DrawColor = Main.DiscoColor;
            }
            else
            {
                DrawColor = Color.White;
            }

            if (!LoadedSprites)
            {
                ActivateButtonIcon = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/ActivateButton").Value;
                ActivateButtonOutline = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/ActivateButtonOutline").Value;
                PowerButtonIcon = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/PowerButtonIcon").Value;

                LoadedSprites = true;
            }

            Vector2 position = GetDimensions().Position();

            if (!IsMouseHovering && DebugUtilityList.DebugMenuEnabled)
            {
                spriteBatch.Draw(ActivateButtonIcon, position, new Color(255, 255, 255, 128));
                spriteBatch.Draw(ActivateButtonOutline, position, new(0, 255, 0));
                spriteBatch.Draw(PowerButtonIcon, position, new(0, 255, 0));
            }
            else
            {
                spriteBatch.Draw(ActivateButtonIcon, position, new Color(255, 255, 255, 128));
                spriteBatch.Draw(ActivateButtonOutline, position, DrawColor);
                spriteBatch.Draw(PowerButtonIcon, position, DrawColor);
            }

            base.Draw(spriteBatch);
        }
    }
}