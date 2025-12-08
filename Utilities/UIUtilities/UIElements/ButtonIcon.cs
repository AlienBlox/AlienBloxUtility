using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
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
        private readonly int ItemID = -1;
        private readonly string _textureLocation;
        private bool _Loaded = false;
        private bool _Locked = false;
        private Color SelectorColor;

        public ButtonIcon(string Key, int itemID, Color selectorColor)
        {
            ItemID = itemID;
            Localization = Language.GetOrRegister(Key);

            _texturePrimary = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/OrbSelector").Value;

            if (ModContent.GetModItem(itemID) == null)
            {
                Main.instance.LoadItem(itemID);
            }

            _textureSecondary = TextureAssets.Item[itemID].Value;

            SelectorColor = selectorColor;
        }

        public ButtonIcon(string Key, string texture, Color selectorColor)
        {
            Localization = Language.GetOrRegister(Key);
            _textureLocation = texture;

            _texturePrimary = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/OrbSelector").Value;
            _textureSecondary = ModContent.Request<Texture2D>(texture).Value;

            SelectorColor = selectorColor;
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
                Color DrawColor = SelectorColor;

                if (Toggle && !IsMouseHovering)
                {
                    DrawColor = new(SelectorColor.R, SelectorColor.G, SelectorColor.B, 128);
                }

                spriteBatch.Draw(_texturePrimary, CenterCalculation - new Vector2(_texturePrimary.Width / 2, _texturePrimary.Height / 2), DrawColor);
            }

            if (IsMouseHovering)
            {
                Main.LocalPlayer.cursorItemIconEnabled = true;
                Main.LocalPlayer.cursorItemIconID = -1;
                Main.LocalPlayer.cursorItemIconText = Localization.Value;
            }

            spriteBatch.Draw(_textureSecondary, CenterCalculation - new Vector2(_textureSecondary.Width / 2, _textureSecondary.Height / 2), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseHovering)
            {
                Main.LocalPlayer.AlienBloxUtility().ItemUsage = false;
            }

            base.Update(gameTime);
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            Main.LocalPlayer.AlienBloxUtility().ItemUsage = true;

            base.MouseOut(evt);
        }

        public void OnHover(UIMouseEvent Event, UIElement ListeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        public void OnToggle(UIMouseEvent Event, UIElement ListeningElement)
        {
            Toggle = !Toggle;
        }

        /// <summary>
        /// Sets the stats for this button
        /// </summary>
        /// <param name="textureSecondary"></param>
        /// <param name="localizationKey"></param>
        /// <param name="buttonColor"></param>
        public void SetStats(string textureSecondary, string localizationKey, Color buttonColor)
        {
            if (_Locked)
            {
                return;
            }

            SelectorColor = buttonColor;
            Localization = Language.GetText(localizationKey);
            _textureSecondary = ModContent.Request<Texture2D>(textureSecondary).Value;

            _Locked = true;
        }

        /// <summary>
        /// Sets the stats for this button
        /// </summary>
        /// <param name="item"></param>
        /// <param name="localizationKey"></param>
        /// <param name="buttonColor"></param>
        public void SetStats(int item, string localizationKey, Color buttonColor)
        {
            if (_Locked)
            {
                return;
            }

            if (ModContent.GetModItem(item) == null)
            {
                Main.instance.LoadItem(item);
            }

            SelectorColor = buttonColor;
            Localization = Language.GetText(localizationKey);
            _textureSecondary = TextureAssets.Item[item].Value;

            _Locked = true;
        }
    }
}