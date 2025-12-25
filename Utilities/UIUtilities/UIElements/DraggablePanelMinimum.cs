using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class DraggablePanelMinimum : UIElement
    {
        private bool _dragging = false;
        private Vector2 _dragOffset;

        public DraggablePanelMinimum():base()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_dragging)
            {
                // Update panel position while dragging
                Left.Set(Main.MouseScreen.X - _dragOffset.X, 0f);
                Top.Set(Main.MouseScreen.Y - _dragOffset.Y, 0f);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.SetUIBase(string.Empty, true, true);

            base.Draw(spriteBatch);
        }

        public override void OnActivate()
        {
            base.OnActivate();
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);

            if (ContainsPoint(evt.MousePosition))
            {
                _dragging = true;
                _dragOffset = evt.MousePosition - new Vector2(Left.Pixels, Top.Pixels);
            }
        }

        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);

            _dragging = false;
        }
    }
}