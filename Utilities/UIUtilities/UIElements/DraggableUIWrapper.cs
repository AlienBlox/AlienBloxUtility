using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class DraggableUIWrapper : UIElement
    {
        public UIPanel connectedPanel;
        private bool _dragging = false;
        private Vector2 _dragOffset;

        public Vector2 sizeOffset = new Vector2(400, 200);

        public Color BackgroundColor;
        public Color BorderColor;

        private float _sizeXScale;
        private float _sizeYScale;

        public Vector2 SizeScale {  get; private set; }

        public DraggableUIWrapper(Vector2 offsetSize, Vector2 scaleSize, Color backgroundC, Color borderC)
        {
            BorderColor = borderC;
            BackgroundColor = backgroundC;

            sizeOffset = offsetSize;
            SetScalePercentage(scaleSize.X, scaleSize.Y);
        }

        public override void OnInitialize()
        {
            connectedPanel = new();
            connectedPanel.SetPadding(0);
            connectedPanel.Left.Set(Main.screenWidth / 2, 0f);  // Initial X
            connectedPanel.Top.Set(Main.screenHeight / 2, 0f);   // Initial Y
            connectedPanel.Width.Set(sizeOffset.X, _sizeXScale);
            connectedPanel.Height.Set(sizeOffset.Y, _sizeYScale);
            connectedPanel.BackgroundColor = Color.CornflowerBlue;

            connectedPanel.BackgroundColor = BackgroundColor;
            connectedPanel.BorderColor = BorderColor;

            connectedPanel.OnLeftMouseDown += MouseDown;
            connectedPanel.OnLeftMouseUp += MouseUp;

            Append(connectedPanel);

            Width = connectedPanel.Width;
            Height = connectedPanel.Height;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            connectedPanel.Width.Set(sizeOffset.X, _sizeXScale);
            connectedPanel.Height.Set(sizeOffset.Y, _sizeYScale);

            Width = connectedPanel.Width;
            Height = connectedPanel.Height;

            if (_dragging)
            {
                // Update panel position while dragging
                connectedPanel.Left.Set(Main.MouseScreen.X - _dragOffset.X, 0f);
                connectedPanel.Top.Set(Main.MouseScreen.Y - _dragOffset.Y, 0f);

                Left = connectedPanel.Left;
                Top = connectedPanel.Top;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.SetUIBase(string.Empty, true);

            base.Draw(spriteBatch);
        }

        public override void OnActivate()
        {
            base.OnActivate();
        }

        public void MouseDown(UIMouseEvent evt, UIElement Element)
        {
            if (connectedPanel.ContainsPoint(evt.MousePosition))
            {
                _dragging = true;
                _dragOffset = evt.MousePosition - new Vector2(connectedPanel.Left.Pixels, connectedPanel.Top.Pixels);
            }
        }

        public void MouseUp(UIMouseEvent evt, UIElement Element)
        {
            _dragging = false;
        }

        public void SetScalePercentage(float x, float y)
        {
            if (x >= 0 && x <= 1)
            {
                _sizeXScale = x;
            }

            if (y >= 0 && y <= 1)
            {
                _sizeYScale = y;
            }
        }

        public Vector2 GetScale()
        {
            return new(_sizeXScale, _sizeYScale);
        }
    }
}