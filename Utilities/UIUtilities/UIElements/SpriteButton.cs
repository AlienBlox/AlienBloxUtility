using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class SpriteButton : UIElement
    {
        public Asset<Texture2D> Texture { get; private set; }

        private readonly string _hoverText;

        private readonly LocalizedText _hoverTextLocalized;

        public SpriteButton(string textureLocation = "AlienBloxUtility/Common/Assets/Placeholder", string HoverText = "Placeholder")
        {
            Texture = ModContent.Request<Texture2D>(textureLocation);

            _hoverText = HoverText;
        }

        public SpriteButton(string textureLocation = "AlienBloxUtility/Common/Assets/Placeholder", LocalizedText HoverText = default)
        {
            Texture = ModContent.Request<Texture2D>(textureLocation);

            _hoverTextLocalized = HoverText;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 Position = GetDimensions().Position();
            Vector2 Dimensions = GetDimensions().Size();
            Vector2 CenterCalculation = Position + Dimensions / 2;

            spriteBatch.Draw(((Texture2D)Texture), CenterCalculation - new Vector2(Texture.Width() / 2, Texture.Height() / 2), Color.White);

            if (IsMouseHovering)
            {
                if (_hoverTextLocalized == null)
                {
                    Main.hoverItemName = _hoverText;
                }
                else
                {
                    Main.hoverItemName = _hoverTextLocalized.Value;
                }
            }

            base.Draw(spriteBatch);
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);

            base.MouseOver(evt);
        }
    }
}