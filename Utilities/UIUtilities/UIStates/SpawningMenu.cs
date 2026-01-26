using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class SpawningMenu : UIState
    {
        public PanelV2 Backpanel;

        public UIElement Backing, OptionBar;

        public UIPanel Sidebar, MainBar, GridContainer, FilterBar;

        public UITextBoxImproved Searchbar;

        public override void OnInitialize()
        {
            Backpanel = new(new(650, 450), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.SpawnMenu").Value, true, false, 650, 450, true);
            Backing = new();
            Sidebar = new();
            MainBar = new();
            OptionBar = new();
            GridContainer = new();
            FilterBar = new();
            Searchbar = new("Search...");

            FilterBar.Width.Set(0, 1);
            FilterBar.Height.Set(0, .5f);
            FilterBar.VAlign = 0;
            FilterBar.HAlign = .5f;

            Searchbar.Width.Set(0, 1);
            Searchbar.Height.Set(0, .5f);
            Searchbar.VAlign = 1;
            Searchbar.HAlign = .5f;

            Sidebar.SetPadding(0);
            MainBar.SetPadding(0);

            OptionBar.Width.Set(0, 1);
            OptionBar.Height.Set(0, .2f);
            OptionBar.HAlign = .5f;
            OptionBar.VAlign = 0;

            GridContainer.Width.Set(0, 1);
            GridContainer.Height.Set(0, .8f);
            GridContainer.HAlign = .5f;
            GridContainer.VAlign = 1;

            Backing.Width.Set(0, 1);
            Backing.Height.Set(-30, 1);
            Backing.VAlign = 1;

            Sidebar.Width.Set(0, .25f);
            Sidebar.Height.Set(0, 1);
            Sidebar.VAlign = .5f;
            Sidebar.HAlign = 1;

            MainBar.Width.Set(0, .75f);
            MainBar.Height.Set(0, 1);
            MainBar.VAlign = .5f;
            MainBar.HAlign = 0;

            OptionBar.Append(Searchbar);
            OptionBar.Append(FilterBar);

            MainBar.Append(OptionBar);
            MainBar.Append(GridContainer);

            Backing.Append(MainBar);
            Backing.Append(Sidebar);

            Backpanel.Append(Backing);
            Append(Backpanel);
        }

        public void CreateFilterButtons()
        {
            UIElement TEButton = new();
            UIElement TileButton = new();
            UIElement ItemButton = new();
            UIElement NPCButton = new();
            UIElement BuffButton = new();
            UIElement ProjectileButton = new();

            TEButton.Width.Set(0, .1f);
            TileButton.Width.Set(0, .1f);
            ItemButton.Width.Set(0, .1f);
            NPCButton.Width.Set(0, .1f);
            BuffButton.Width.Set(0, .1f);
            ProjectileButton.Width.Set(0, .1f);
        }
    }
}