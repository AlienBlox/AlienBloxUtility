using AlienBloxUtility.Utilities.DataStorage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DebugPanelStack : UIState
    {
        private UIElement _myPanel;
        private Texture2D _nineSliceTexture;
        private bool _Loaded = false;

        public override void OnInitialize()
        {
            _nineSliceTexture = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/UIGridGray").Value; // Replace with your actual texture path

            _myPanel = new UIElement();
            _myPanel.Width.Set(256f, 0f); // Set width
            _myPanel.Height.Set(128f, 0f); // Set height
            _myPanel.HAlign = .5f;
            _myPanel.VAlign = 0.05f;
            Append(_myPanel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!_Loaded)
            {
                _nineSliceTexture = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/UIGridGray").Value;
            }
            // Draw 9-slice texture
            this.DrawNineSlice(spriteBatch, _nineSliceTexture, _myPanel.GetDimensions().Size().X, _myPanel.GetDimensions().Size().Y, _myPanel.Width.Pixels, _myPanel.Height.Pixels, Color.White);
        }
    }
}