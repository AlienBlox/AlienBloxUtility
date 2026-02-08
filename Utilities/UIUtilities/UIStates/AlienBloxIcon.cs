using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class AlienBloxIcon : UIState
    {
        public UIElement BackingIcon;

        public UIItemIcon BackingSlot;

        private Item _item;

        public override void OnInitialize()
        {
            _item = new Item();

            BackingIcon = new();
            BackingSlot = new(_item, false);

            BackingIcon.Width.Set(40, 0);
            BackingIcon.Height.Set(40, 0);
            BackingSlot.Width.Set(0, 1);
            BackingSlot.Height.Set(0, 1);

            BackingIcon.Append(BackingSlot);

            Append(BackingIcon);
        }

        public override void Update(GameTime gameTime)
        {
            BackingIcon.Left.Set(Main.MouseScreen.X, 0);
            BackingIcon.Height.Set(Main.MouseScreen.Y, 0);

            base.Update(gameTime);
        }

        public void SetItem(int icon)
        {
            _item.type = icon;
        }
    }
}