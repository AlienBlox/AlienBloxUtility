using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class ButtonIcon : UIElement
    {
        public bool Toggle = false;
        public LocalizedText Localization { get; private set; }
        private Texture2D _texturePrimary;
        private Texture2D _textureSecondary;
        private int ItemID = -1;
        private string _textureLocation;
        private bool _Loaded = false;

        public ButtonIcon(string Key, int itemID)
        {
            ItemID = itemID;
            Localization = Language.GetOrRegister(Key);

            _texturePrimary = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/OrbSelector").Value;

            if (ModContent.GetModItem(itemID) == null)
            {
                Main.instance.LoadItem(itemID);
            }

            _textureSecondary = TextureAssets.Item[itemID].Value;
        }

        public ButtonIcon(string Key, string texture)
        {
            Localization = Language.GetOrRegister(Key);
            _textureLocation = texture;

            _texturePrimary = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/OrbSelector").Value;
            _textureSecondary = ModContent.Request<Texture2D>(texture).Value;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 Position = GetDimensions().Position();
            Vector2 Dimensions = GetDimensions().Size();
            Vector2 CenterCalculation = Position + Dimensions / 2;

            if (!_Loaded)
            {
                _texturePrimary = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/OrbSelector").Value;

                if (ItemID != -1)
                {
                    if (ModContent.GetModItem(ItemID) == null)
                    {
                        Main.instance.LoadItem(ItemID);
                    }

                    _textureSecondary = TextureAssets.Item[ItemID].Value;
                }
                else
                {
                    _textureSecondary = ModContent.Request<Texture2D>(_textureLocation).Value;
                }

                _Loaded = true;
            }

            if (IsMouseHovering || Toggle)
            {
                spriteBatch.Draw(_texturePrimary, CenterCalculation - new Vector2(_texturePrimary.Width / 2, _texturePrimary.Height / 2), Color.White);
            }

            if (IsMouseHovering)
            {
                Main.LocalPlayer.cursorItemIconEnabled = true;
                Main.LocalPlayer.cursorItemIconID = -1;
                Main.LocalPlayer.cursorItemIconText = Localization.Value;
            }

            spriteBatch.Draw(_textureSecondary, CenterCalculation - new Vector2(_textureSecondary.Width / 2, _textureSecondary.Height / 2), Color.White);
        }
    }
}