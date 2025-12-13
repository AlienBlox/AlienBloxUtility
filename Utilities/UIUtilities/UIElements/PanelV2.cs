using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class PanelV2 : UIPanel
    {
        private bool isResizing;
        private Vector2 originalMousePosition;
        private int originalPanelWidth;
        private int originalPanelHeight;

        // Minimum size for the panel
        private int minWidth = 100;
        private int minHeight = 75;

        private bool _dragging = false;
        private Vector2 _dragOffset;

        public UIElement Close;

        public UIText Text;

        public UIElement Dragger;

        public UIPanel Topbar;

        public Vector2 sizeOffset = new Vector2(400, 200);

        public Color BackgroundColorOverride;
        public Color BorderColorOverride;

        private float _sizeXScale;
        private float _sizeYScale;

        private string Title;

        private readonly bool effectsForUIChild;

        public LocalizedText Locale { get; private set; }

        public Vector2 SizeScale { get; private set; }

        public PanelV2(Vector2 offsetSize, Vector2 scaleSize, Color backgroundC, Color borderC, string title = "Placeholder", bool Localizated = false, bool ApplyEffects = true, int minWidth = 300, int minHeight = 300)
        {
            BorderColorOverride = borderC;
            BackgroundColorOverride = backgroundC;

            effectsForUIChild = ApplyEffects;

            if (Localizated)
            {
                Locale = Language.GetText(title);
            }

            this.minHeight = minHeight;
            this.minWidth = minWidth;
            Title = title;
            sizeOffset = offsetSize;
            SetScalePercentage(scaleSize.X, scaleSize.Y);
        }

        public override void OnInitialize()
        {
            Topbar = new();
            Topbar.VAlign = 0;
            Topbar.HAlign = .5f;
            Topbar.Height.Set(34, 0);
            Topbar.Width.Set(0, 1);
            Topbar.BackgroundColor = new(BackgroundColorOverride.R, BackgroundColorOverride.G, BackgroundColorOverride.B, 0);
            Topbar.BorderColor = new(BorderColorOverride.R, BorderColorOverride.G, BorderColorOverride.B, 255);

            Text = new(Title);
            Text.Width.Set(0, 1);
            Text.Height.Set(0, 1);

            Close = new();
            Close.Width.Set(34, 0);
            Close.Height.Set(34, 0);
            Close.VAlign = 0f;
            Close.HAlign = 0f;

            Left.Set(Main.screenWidth / 2, 0f);  // Initial X
            Top.Set(Main.screenHeight / 2, 0f);   // Initial Y
            Width.Set(sizeOffset.X, _sizeXScale);
            Height.Set(sizeOffset.Y, _sizeYScale);
            BackgroundColor = BackgroundColorOverride;
            BorderColor = BorderColorOverride;

            OnLeftMouseUp += MouseUp;
            OnLeftMouseDown += MouseDown;

            var resizeCorner = new UIPanel();
            resizeCorner.Width.Set(50, 0f);
            resizeCorner.Height.Set(50, 0f);
            resizeCorner.Top.Set(0, 0f);
            resizeCorner.Left.Set(1f, 1f); // Place it in the top-right corner
            resizeCorner.OnLeftMouseDown += ResizeCorner_OnMouseDown;
            resizeCorner.OnLeftMouseUp += ResizeCorner_OnMouseUp;
            resizeCorner.OnMouseOver += (evt, element) => Main.hoverItemName = "Resize";

            Topbar.Append(Text);

            SetPadding(0);
            Append(Close);
            Append(Topbar);
            Append(resizeCorner);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isResizing)
            {
                Vector2 mouseDelta = new Vector2(Main.mouseX, Main.mouseY) - originalMousePosition;

                // Calculate new width and height based on mouse movement
                int newWidth = (int)(originalPanelWidth + mouseDelta.X);
                int newHeight = (int)(originalPanelHeight + mouseDelta.Y);

                // Enforce the minimum size
                if (newWidth < minWidth)
                    newWidth = minWidth;
                if (newHeight < minHeight)
                    newHeight = minHeight;

                // Update the panel size with the adjusted values
                Width.Set(newWidth, 0f);
                Height.Set(newHeight, 0f);
            }

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

            if (Close != null)
            {
                Vector2 Pos = Close.GetDimensions().Position();
                spriteBatch.Draw(ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/CloseButtonV2").Value, Pos, Color.White);

                if (Close.ContainsPoint(Main.MouseScreen))
                {
                    Main.hoverItemName = "Close";
                }
            }
        }

        public override void OnActivate()
        {
            base.OnActivate();
        }

        private void ResizeCorner_OnMouseDown(UIMouseEvent evt, UIElement listeningElement)
        {
            isResizing = true;
            originalMousePosition = new Vector2(Main.mouseX, Main.mouseY);
            originalPanelWidth = (int)Width.Pixels;
            originalPanelHeight = (int)Height.Pixels;
        }

        private void ResizeCorner_OnMouseUp(UIMouseEvent evt, UIElement listeningElement)
        {
            isResizing = false;
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