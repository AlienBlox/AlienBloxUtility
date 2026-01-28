using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using FixedUIScrollbar = Terraria.ModLoader.UI.Elements.FixedUIScrollbar;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class ItemDisplay : UIPanel
    {
        public UIItemIcon Icon;

        public Item AssociatedItem;

        public ItemDisplay(int item)
        {
            Width.Set(40, 0);
            Height.Set(40, 0);

            AssociatedItem = new(item);
            Icon = new(AssociatedItem, false);
            Icon.Width.Set(0, 1);
            Icon.Height.Set(0, 1);

            Append(Icon);
        }

        public UIElement CardGen()
        {
            UIElement P = new();
            UIList List = [];
            FixedUIScrollbar scroller = new(UserInterface.ActiveInstance)
            {
                VAlign = .5f,
                HAlign = 1
            };

            scroller.Height.Set(-10, 1);
            scroller.VAlign = .5f;

            List.Width.Set(0, 1);
            List.Height.Set(0, 1);
            List.ManualSortMethod = (_) => { };
            List.Append(scroller);
            List.SetScrollbar(scroller);

            foreach (string s in UIUtilities.LoadCard(AssociatedItem))
            {
                UIText t = List.AddTextEntry(s);

                t.IsWrapped = true;
                t.DynamicallyScaleDownToWidth = true;
            }

            P.Width.Set(0, 1);
            P.Height.Set(0, 1);
            P.Append(List);

            return P;
        }
    }
}