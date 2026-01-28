using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using FixedUIScrollbar = Terraria.ModLoader.UI.Elements.FixedUIScrollbar;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class SpawningMenu : UIState
    {
        public enum MenuSwitch : int
        {
            Item,
            NPC,
            Projectile,
            Buff,
            Tile,
            TE,
        }

        public PanelV2 Backpanel;

        public UIElement Backing, OptionBar;

        public UIPanel Sidebar, MainBar, GridContainer, CardContainer, CardTitle/*, FilterBar*/;

        public ButtonIcon SendButton, SwitchModes;

        public UITextBoxImproved Searchbar;

        public UIGrid ContentGrid;

        public FixedUIScrollbar Scroller;

        public List<ItemDisplay> CachedItems;

        private int _menuSwitch;

        public MenuSwitch SwitchState { get { return (MenuSwitch)_menuSwitch; } set { _menuSwitch = (int)value; } }

        public override void OnInitialize()
        {
            Backpanel = new(new(650, 450), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.SpawnMenu").Value, true, false, 650, 450, true);
            Backing = new();
            Sidebar = new();
            MainBar = new();
            OptionBar = new();
            GridContainer = new();
            CardContainer = new();
            CardTitle = new();
            SendButton = new("Mods.AlienBloxUtility.UI.GeneralSend", ItemID.PaperAirplaneA, Color.White);
            SwitchModes = new("Mods.AlienBloxUtility.UI.SwitchSpawnMode", ItemID.Switch, Color.DarkSlateGray);
            Scroller = new(UserInterface.ActiveInstance);
            ContentGrid = [];
            //FilterBar = new();
            Searchbar = new("Search...");

            CardTitle.Width.Set(0, 1);
            CardTitle.Height.Set(0, .1f);
            CardTitle.VAlign = 0;
            CardTitle.HAlign = .5f;
            CardTitle.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.Cards"));

            CardContainer.Width.Set(0, 1);
            CardContainer.Height.Set(0, .9f);
            CardContainer.VAlign = 1;
            CardContainer.HAlign = .5f;
            CardContainer.BackgroundColor = Color.LightBlue;

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
            ContentGrid.VAlign = ContentGrid.HAlign = .5f;
            ContentGrid.ManualSortMethod = (_) => { };
            ContentGrid.Append(Scroller);
            ContentGrid.SetScrollbar(Scroller);

            //ContentGrid.InsertText("wee");

            SendButton.Width.Set(0, .1f);
            SendButton.Height.Set(0, 1);
            SendButton.VAlign = .5f;
            SendButton.HAlign = .9f;
            SendButton.OnLeftClick += SearchFunction;

            SwitchModes.Width.Set(0, .1f);
            SwitchModes.Height.Set(0, 1f);
            SwitchModes.VAlign = .5f;
            SwitchModes.HAlign = 1;
            SwitchModes.OnLeftClick += (_, _) =>
            {
                switch (_menuSwitch)
                {
                    case 0:
                        _menuSwitch = 1;
                        PopulateNPCEasy();
                        break;
                    case 1:
                        _menuSwitch = 2;
                        break;
                    case 2:
                        _menuSwitch = 3;
                        break;
                    case 3:
                        _menuSwitch = 4;
                        break;
                    case 4: 
                        _menuSwitch = 5;
                        break;
                    case 5:
                        _menuSwitch = 0;
                        try
                        {
                            PopulateItemsAsync();
                        }
                        catch
                        {

                        }
                        break;
                }
            };

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

            Sidebar.Append(CardTitle);
            Sidebar.Append(CardContainer);

            Backing.Append(MainBar);
            Backing.Append(Sidebar);

            Backpanel.Append(Backing);
            Append(Backpanel);

            //PopulateItems();
            //CreateFilterButtons();
        }

        public void SearchFunction(UIMouseEvent evt, UIElement elem)
        {
            Task.Run(() =>
            {
                ContentGrid.Clear();

                lock (ContentGrid)
                {
                    switch ((int)SwitchState)
                    {
                        case 0: //item
                            var display = PopulateItems(false);

                            ContentGrid.Clear();
                            ContentGrid.AddRange(display);
                            break;
                        case 1: //npc
                            ContentGrid.Clear();
                            ContentGrid.AddRange(PopulateNPC());
                            break;
                        case 2: //projectile
                            break;
                        case 3: //buff
                            break;
                        case 4: //tile
                            break;
                        case 5: //tile entity
                            break;
                    }
                }

                Searchbar.SetText(string.Empty);
            });
        }

        private SmartNPCDisplay[] PopulateNPC()
        {
            List<SmartNPCDisplay> items = [];
            int count = NPCLoader.NPCCount;

            for (int i = 0; i < count; i++)
            {
                items.Add(new(i));
            }

            IEnumerable<SmartNPCDisplay> clean = items.Where(target => ContentIDToString.NPCIdToString(target.NPCType).Contains(Searchbar.Text));

            return [.. clean];
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        private ItemDisplay[] PopulateItems(bool insert = true)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if (insert)
                ContentGrid.Clear();

            List<ItemDisplay> items = [];

            int count = ItemLoader.ItemCount;

            for (int i = 0; i < count; i++)
            {
                try
                {
                    if (ContentIDToString.ItemIdToString(i).Contains(Searchbar.Text))
                    {
                        ItemDisplay itemDisplay = new(i);

                        itemDisplay.OnRightClick += (_, _) =>
                        {
                            try
                            {
                                Main.LocalPlayer.QuickSpawnItem(new EntitySource_Misc("HacksBlox"), itemDisplay.AssociatedItem.type, 1);
                            }
                            catch
                            {

                            }
                        };

                        itemDisplay.OnLeftClick += (_, _) =>
                        {
                            try
                            {
                                Main.LocalPlayer.QuickSpawnItem(new EntitySource_Misc("HacksBlox"), itemDisplay.AssociatedItem.type, itemDisplay.AssociatedItem.maxStack);
                            }
                            catch
                            {

                            }
                        };

                        itemDisplay.OnMiddleClick += (_, _) =>
                        {
                            CardContainer.RemoveAllChildren();

                            try
                            {
                                CardContainer.Append(itemDisplay.CardGen());
                            }
                            catch
                            {

                            }
                        };

                        items.Add(itemDisplay);

                        if (insert)
                            ContentGrid.Add(itemDisplay);
                    }
                }
                catch
                {

                }
            }

            return [.. items];
        }

        private void PopulateNPCEasy()
        {
            Task.Run(() =>
            {
                lock (ContentGrid)
                {
                    ContentGrid.Clear();

                    ContentGrid.AddRange(PopulateNPC());
                }
            });
        }

        private void PopulateItemsAsync()
        {
            Task.Run(() =>
            {
                lock (GridContainer)
                {
                    var display = PopulateItems(false);

                    ContentGrid.Clear();
                    ContentGrid.AddRange(display);
                }
            });
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