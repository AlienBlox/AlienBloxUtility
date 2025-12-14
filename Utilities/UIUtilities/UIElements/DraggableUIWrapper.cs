using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class DraggableUIWrapper : UIPanel
    {
        private bool _dragging = false;
        private Vector2 _dragOffset;

        public CloseButton Close;

        public UIText Text;

        public Vector2 sizeOffset = new Vector2(400, 200);

        public Color BackgroundColorOverride;
        public Color BorderColorOverride;

        private float _sizeXScale;
        private float _sizeYScale;

        private string Title;

        private readonly bool effectsForUIChild;

        public LocalizedText Locale { get; private set; }

        public Vector2 SizeScale { get; private set; }

        public DraggableUIWrapper(Vector2 offsetSize, Vector2 scaleSize, Color backgroundC, Color borderC, string title = "Placeholder", bool Localizated = false, bool ApplyEffects = true)
        {
            BorderColorOverride = borderC;
            BackgroundColorOverride = backgroundC;

            effectsForUIChild = ApplyEffects;

            if (Localizated)
            {
                Locale = Language.GetText(title);
            }

            Title = title;
            sizeOffset = offsetSize;
            SetScalePercentage(scaleSize.X, scaleSize.Y);
        }

        public override void OnInitialize()
        {
            Text = new(Title);
            Text.Width.Set(0, 100);
            Text.Height.Set(25, 0);

            Close = new CloseButton();

            Left.Set(Main.screenWidth / 2, 0f);  // Initial X
            Top.Set(Main.screenHeight / 2, 0f);   // Initial Y
            Width.Set(sizeOffset.X, _sizeXScale);
            Height.Set(sizeOffset.Y, _sizeYScale);
            BackgroundColor = BackgroundColorOverride;
            BorderColor = BorderColorOverride;

            OnLeftMouseUp += MouseUp;
            OnLeftMouseDown += MouseDown;

            Append(Text);
            Append(Close);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Text != null && Locale != null)
            {
                Text?.SetText(Locale?.Value);
            }
            else if (Text != null && Locale == null)
            {
                Text.SetText(Title);
            }

            Width.Set(sizeOffset.X, _sizeXScale);
            Height.Set(sizeOffset.Y, _sizeYScale);

            BackgroundColor = BackgroundColorOverride;
            BorderColor = BorderColorOverride;

            if (_dragging)
            {
                // Update panel position while dragging
                Left.Set(Main.MouseScreen.X - _dragOffset.X, 0f);
                Top.Set(Main.MouseScreen.Y - _dragOffset.Y, 0f);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.SetUIBase(string.Empty, effectsForUIChild);

            base.Draw(spriteBatch);
        }

        public override void OnActivate()
        {
            base.OnActivate();
        }

        public void MouseDown(UIMouseEvent evt, UIElement Element)
        {
            if (ContainsPoint(evt.MousePosition))
            {
                _dragging = true;
                _dragOffset = evt.MousePosition - new Vector2(Left.Pixels, Top.Pixels);
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