using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class ButtonIcon : UIElement
    {
        public LocalizedText Title;

        private Texture2D ButtonBar;
        private Texture2D ButtonOutline;

        public bool ButtonClick;
        Color ButtonColor = Color.White;
        
        private UIText TitleBar;

        public ButtonIcon(string LocalizationKey)
        {
            Title = Language.GetOrRegister(LocalizationKey);
        }

        public override void OnInitialize()
        {
            if (Title != null)
            {
                TitleBar = new(Title);
            }

            Width.Set(0, Parent.Children.Count());
            Height.Set(0, 100);
            VAlign = 0.5f;
            HAlign = 1 / Parent.Children.Count();

            Append(TitleBar);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 Position = GetDimensions().Position();

            if (IsMouseHovering)
            {
                ButtonColor = Main.DiscoColor;
            }
            else
            {
                ButtonColor = Color.Black;
            }

            if (ButtonBar == null || ButtonOutline == null)
            {
                ButtonBar = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/ButtonIcon").Value;
                ButtonOutline = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/ButtonOutline").Value;
            }

            spriteBatch.Draw(ButtonBar, Position, new Color(255, 255, 255, 128));

            if (!IsMouseHovering && ButtonClick)
            {
                spriteBatch.Draw(ButtonOutline, Position, new Color(0, 255, 0));
            }
            else
            {
                spriteBatch.Draw(ButtonOutline, Position, ButtonColor);
            }

            base.Draw(spriteBatch);
        }
    }
}