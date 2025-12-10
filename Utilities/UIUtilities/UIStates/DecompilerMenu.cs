using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DecompilerMenu : UIState
    {
        public DraggableUIWrapper panel;
        private bool dragging = false;
        private Vector2 dragOffset;

        public override void OnInitialize()
        {
            panel = new(new Vector2(300, 300), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0));

            Append(panel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}