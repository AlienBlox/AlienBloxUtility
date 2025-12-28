using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
        private float originalWidth;
        private float originalHeight;
        private Vector2 originalMousePosition;
        private float minWidth = 100f;
        private float minHeight = 50f;
        private Vector2 Resize;

        private bool _dragging = false;
        private Vector2 _dragOffset;

        public UIElement Close, LockUI, Dragger;

        public UIText Text;

        public UIPanel Topbar;

        public Vector2 sizeOffset = new Vector2(400, 200);

        public Color BackgroundColorOverride;
        public Color BorderColorOverride;

        private float _sizeXScale;
        private float _sizeYScale;

        private string Title;

        private bool MenuLock = false;

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
            Resize = offsetSize;
            Title = title;
            sizeOffset = offsetSize;
            SetScalePercentage(scaleSize.X, scaleSize.Y);
        }

        public override void OnInitialize()
        {
            Topbar = new()
            {
                VAlign = 0,
                HAlign = .5f
            };
            Topbar.Height.Set(0, .1f);
            Topbar.Width.Set(0, 1);
            Topbar.BackgroundColor = new(BackgroundColorOverride.R, BackgroundColorOverride.G, BackgroundColorOverride.B, 0);
            Topbar.BorderColor = new(BorderColorOverride.R, BorderColorOverride.G, BorderColorOverride.B, 255);
            Topbar.MaxHeight.Set(34, 0);
            Topbar.SetPadding(0);

            Text = new(Title);
            Text.Width.Set(0, 1);
            Text.Height.Set(0, 1);
            Text.VAlign = 0.5f;

            LockUI = new();
            LockUI.Width.Set(0, .1f);
            LockUI.Height.Set(0, 1f);
            LockUI.MaxWidth.Set(34, 0);
            LockUI.VAlign = 0f;
            LockUI.HAlign = 0f;
            LockUI.MarginLeft = 34;
            LockUI.OnLeftClick += UILock;

            Close = new();
            Close.Width.Set(0, .1f);
            Close.Height.Set(0, 1f);
            Close.MaxWidth.Set(34, 0);
            Close.VAlign = 0f;
            Close.HAlign = 0f;

            Left.Set(Main.screenWidth / 2, 0f);  // Initial X
            Top.Set(Main.screenHeight / 2, 0f);   // Initial Y
            Width.Set(sizeOffset.X, _sizeXScale);
            Height.Set(sizeOffset.Y, _sizeYScale);
            BackgroundColor = BackgroundColorOverride;
            BorderColor = BorderColorOverride;

            Topbar.Append(Text);
            Topbar.Append(Close);
            Topbar.Append(LockUI);

            SetPadding(0);

            Append(Topbar);

            Text.TextOriginY += 0.5f;

            OnLeftMouseDown += MouseDown;
            OnLeftMouseUp += MouseUp;

            var resizeHandle = new UIElement();
            resizeHandle.Width.Set(0, 0.1f);
            resizeHandle.Height.Set(0, 0.1f);
            resizeHandle.MaxHeight.Set(50, 0);
            resizeHandle.MaxWidth.Set(50, 0);
            resizeHandle.Left.Set(0, 0f);  // Bottom-right corner
            resizeHandle.Top.Set(0, 0f);
            resizeHandle.VAlign = 1;
            resizeHandle.HAlign = 1;
            resizeHandle.OnLeftMouseDown += ResizeHandle_OnMouseDown;
            resizeHandle.OnLeftMouseUp += ResizeHandle_OnMouseUp;

            Append(resizeHandle);
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

            if (_dragging && !MenuLock)
            {
                // Update panel position while dragging
                Left.Set(Main.MouseScreen.X - _dragOffset.X, 0f);
                Top.Set(Main.MouseScreen.Y - _dragOffset.Y, 0f);
            }

            if (isResizing)
            {
                float deltaX = Main.mouseX - originalMousePosition.X;
                float deltaY = Main.mouseY - originalMousePosition.Y;

                Resize = new(Math.Max(originalWidth + deltaX, minWidth), Math.Max(originalHeight + deltaY, minHeight));
            }

            Width.Set(Resize.X, 0);
            Height.Set(Resize.Y, 0);
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
                    Main.hoverItemName = Language.GetTextValue("Mods.AlienBloxUtility.UI.CloseUI");
                }
            }

            if (LockUI != null)
            {
                Vector2 Pos = LockUI.GetDimensions().Position();
                spriteBatch.Draw(ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/MenuLock").Value, Pos, Color.White);

                if (LockUI.ContainsPoint(Main.MouseScreen))
                {
                    Main.hoverItemName = Language.GetTextValue("Mods.AlienBloxUtility.UI.LockUI");
                }

                if (MenuLock)
                {
                    spriteBatch.Draw(ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/ButtonV2Outline").Value, Pos, Main.DiscoColor);
                }
            }
        }

        public override void OnActivate()
        {
            base.OnActivate();
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

        public void UILock(UIMouseEvent evt, UIElement element)
        {
            MenuLock = !MenuLock;
        }

        private void ResizeHandle_OnMouseDown(UIMouseEvent evt, UIElement listeningElement)
        {
            if (MenuLock)
            {
                return;
            }

            _dragging = false;
            isResizing = true;
            originalWidth = Width.Pixels;
            originalHeight = Height.Pixels;
            originalMousePosition = new Vector2(Main.mouseX, Main.mouseY);
        }

        private void ResizeHandle_OnMouseUp(UIMouseEvent evt, UIElement listeningElement)
        {
            isResizing = false;
        }
    }
}