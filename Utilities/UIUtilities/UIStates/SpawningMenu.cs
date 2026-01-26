using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using FixedUIScrollbar = Terraria.ModLoader.UI.Elements.FixedUIScrollbar;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class SpawningMenu : UIState
    {
        public PanelV2 Backpanel;

        public UIElement Backing, OptionBar;

        public UIPanel Sidebar, MainBar, GridContainer/*, FilterBar*/;

        public ButtonIcon SendButton, SwitchModes;

        public UITextBoxImproved Searchbar;

        public UIGrid ContentGrid;

        public FixedUIScrollbar Scroller;

        public override void OnInitialize()
        {
            Backpanel = new(new(650, 450), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.SpawnMenu").Value, true, false, 650, 450, true);
            Backing = new();
            Sidebar = new();
            MainBar = new();
            OptionBar = new();
            GridContainer = new();
            SendButton = new("Mods.AlienBloxUtility.UI.GeneralSend", ItemID.PaperAirplaneA, Color.White);
            SwitchModes = new("Mods.AlienBloxUtility.UI.SwitchSpawnMode", ItemID.Switch, Color.DarkSlateGray);
            Scroller = new(UserInterface.ActiveInstance);
            ContentGrid = [];
            //FilterBar = new();
            Searchbar = new("Search...");

            /*
            FilterBar.Width.Set(-10, 1);
            FilterBar.Height.Set(-10, .5f);
            FilterBar.VAlign = 0;
            FilterBar.HAlign = .5f;
            FilterBar.Top.Set(5, 0);
            */

            Scroller.Height.Set(-10, 1);
            Scroller.VAlign = .5f;
            Scroller.HAlign = 1;

            ContentGrid.Width.Set(0, 1);
            ContentGrid.Height.Set(0, 1);
            ContentGrid.Append(Searchbar);
            ContentGrid.SetScrollbar(Scroller);

            ContentGrid.InsertText("wee");

            SendButton.Width.Set(0, .1f);
            SendButton.Height.Set(0, 1);
            SendButton.VAlign = .5f;
            SendButton.HAlign = .9f;

            SwitchModes.Width.Set(0, .1f);
            SwitchModes.Height.Set(0, 1f);
            SwitchModes.VAlign = .5f;
            SwitchModes.HAlign = 1;

            Searchbar.Width.Set(-10, 1);
            Searchbar.Height.Set(-10, 1f);
            Searchbar.Top.Set(-2.5f, 0);
            Searchbar.VAlign = 1;
            Searchbar.HAlign = .5f;

            Sidebar.SetPadding(0);
            MainBar.SetPadding(0);

            OptionBar.Width.Set(0, 1);
            OptionBar.Height.Set(0, .1f);
            OptionBar.HAlign = .5f;
            OptionBar.VAlign = 0;

            GridContainer.Width.Set(0, 1);
            GridContainer.Height.Set(0, .9f);
            GridContainer.HAlign = .5f;
            GridContainer.VAlign = 1;

            Backing.Width.Set(0, 1);
            Backing.Height.Set(-32, 1);
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
            //OptionBar.Append(FilterBar);

            Searchbar.Append(SendButton);
            Searchbar.Append(SwitchModes);

            MainBar.Append(OptionBar);
            MainBar.Append(GridContainer);

            GridContainer.SetPadding(0);
            GridContainer.Append(ContentGrid);

            Backing.Append(MainBar);
            Backing.Append(Sidebar);

            Backpanel.Append(Backing);
            Append(Backpanel);

            //CreateFilterButtons();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.DrawChildren(spriteBatch);
        }


        public void PopulateWithDummy()
        {
            ContentGrid.Clear();
            
            for (int i = 0; i < 100; i++)
            {
                UIPanel p = new();

                p.Width.Set(30, 0);
                p.Height.Set(30, 0);

                ContentGrid.Add(p);
            }
        }

        /*
        public void CreateFilterButtons()
        {
            UIElement TEButton = new();
            UIElement TileButton = new();
            UIElement ItemButton = new();
            UIElement NPCButton = new();
            UIElement BuffButton = new();
            UIElement ProjectileButton = new();

            TEButton.Width.Set(0, 1f / 6f);
            TEButton.Height.Set(0, 1);
            TEButton.HAlign = (1 / 6);

            TileButton.Width.Set(0, 1f / 6f);
            TileButton.Height.Set(0, 1);
            TileButton.HAlign = (1 / 6) * 2;

            ItemButton.Width.Set(0, 1f / 6f);
            ItemButton.Height.Set(0, 1);
            ItemButton.HAlign = (1 / 6) * 3;

            NPCButton.Width.Set(0, 1f / 6f);
            NPCButton.Height.Set(0, 1);
            NPCButton.HAlign = (1 / 6) * 4;

            BuffButton.Width.Set(0, 1f / 6f);
            BuffButton.Height.Set(0, 1);
            BuffButton.HAlign = (1 / 6) * 5;

            ProjectileButton.Width.Set(0, 1f / 6f);
            ProjectileButton.Height.Set(0, 1);
            ProjectileButton.HAlign = (1 / 6) * 6;

            OptionBar.Append(TEButton);
            OptionBar.Append(TileButton);
            OptionBar.Append(ItemButton);
            OptionBar.Append(NPCButton);
            OptionBar.Append(BuffButton);
            OptionBar.Append(ProjectileButton);
        }
        */
    }
}