using Microsoft.Xna.Framework;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class ConHostMenu : UIElement
    {
        public DraggableUIWrapper UIHandler;


        public Color BackgroundC {  get; private set; }

        public Color BorderC { get; private set; }

        public string Title { get; private set; }

        public bool Localized { get; private set; }

        public ConHostMenu(Color backgroundC, Color borderC, string title = "Placeholder", bool localizated = false)
        {
            BackgroundC = backgroundC;
            BorderC = borderC;
            Title = title;
            Localized = localizated;
        }

        public override void OnInitialize()
        {
            UIHandler = new(new(750, 500), Vector2.Zero, BackgroundC, BorderC, Title, Localized);

            Width.Set(750, 0);
            Height.Set(500, 0);
        }
    }
}