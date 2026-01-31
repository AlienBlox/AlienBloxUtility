using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            Wall
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

        private bool _fix;

        public MenuSwitch SwitchState { get { return (MenuSwitch)_menuSwitch; } set { _menuSwitch = (int)value; } }

        public override void OnInitialize()
        {
            Backpanel = new(new(650, 450), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.SpawnMenu").Value, true, false, 650, 450);

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
                Task.Run(() => {
                try
                {
                    lock (ContentGrid)
                    {
                        switch (_menuSwitch)
                        {
                            case 0:
                                _menuSwitch = 1;
                                PopulateNPCEasy();
                                break;
                            case 1:
                                _menuSwitch = 2;
                                PopulateProjectileEasy();
                                break;
                            case 2:
                                _menuSwitch = 3;
                                PopulateBuffEasy();
                                break;
                            case 3:
                                _menuSwitch = 4;
                                PopulateTileEasy();
                                break;
                            case 4:
                                _menuSwitch = 5;
                                PopulateTEEasy();
                                break;
                            case 5:
                                _menuSwitch = 6;
                                PopulateWallEasy();
                                break;
                            case 6:
                                _menuSwitch = 0;
                                PopulateItemsAsync();
                                break;
                        }
                    }

                    Main.NewText($"Current Mode: {Enum.GetName(SwitchState)}");
                }
                catch
                {

                }});
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

        public override void OnActivate()
        {
            /*
            if (Backpanel.Close != null)
            {
                Backpanel.Close.OnLeftClick += (_, _) =>
                {
                    SpawningMenuRender.SpawnMenuEnabled = false;
                    DebugSidebarRender.Instance.Element.SpawningTool.Toggle = false;
                };
            }
            else
            {
                Backpanel.Close = new();
                Backpanel.Close.Width.Set(0, .1f);
                Backpanel.Close.Height.Set(0, 1f);
                Backpanel.Close.MaxWidth.Set(34, 0);
                Backpanel.Close.VAlign = 0f;
                Backpanel.Close.HAlign = 0f;
                Backpanel.Close.OnLeftClick += (_, _) =>
                {
                    SpawningMenuRender.SpawnMenuEnabled = false;
                    DebugSidebarRender.Instance.Element.SpawningTool.Toggle = false;
                };
            }
            */
        }

        public override void Update(GameTime gameTime)
        {
            if (!_fix)
            {
                PopulateItemsAsync();

                Backpanel.Close.OnLeftClick += (_, _) =>
                {
                    try
                    {
                        SpawningMenuRender.SpawnMenuEnabled = false;
                        DebugSidebarRender.Instance.Element.SpawningTool.Toggle = false;
                    }
                    catch
                    {

                    }
                };

                _fix = true;
            }

            base.Update(gameTime);
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
                            var populate = PopulateNPC();

                            ContentGrid.Clear();
                            ContentGrid.AddRange(populate);
                            break;
                        case 2: //projectile
                            PopulateProjectileEasy();
                            break;
                        case 3: //buff
                            PopulateBuffEasy();
                            break;
                        case 4: //tile
                            PopulateTileEasy();
                            break;
                        case 5: //tile entity
                            PopulateTEEasy();
                            break;
                        case 6: //walls
                            PopulateWallEasy();
                            break;
                    }
                }

                Searchbar.SetText(string.Empty);
            });
        }

        private void PopulateTEEasy()
        {
            List<SmartTEDisplay> items = [];
            int count = TileEntity.manager.EnumerateEntities().Count;

            for (int i = 0; i < count; i++)
            {
                SmartTEDisplay e = new(i);

                /*
                e.OnMiddleClick += (_, _) =>
                {
                    e.GenCard(CardContainer);
                };
                */

                items.Add(e);
            }

            IEnumerable<SmartTEDisplay> clean = items.Where(target => ContentIDToString.TEToString(target.TEID).Contains(Searchbar.Text));

            var temp = clean.ToList();

            temp.Add(new(-1));

            clean = temp;

            ContentGrid.Clear();
            ContentGrid.AddRange(clean);
        }

        private void PopulateWallEasy()
        {
            List<SmartWallDisplay> items = [];
            int count = WallLoader.WallCount;

            for (int i = 0; i < count; i++)
            {
                SmartWallDisplay e = new(i);

                /*
                e.OnMiddleClick += (_, _) =>
                {
                    e.GenCard(CardContainer);
                };
                */

                items.Add(e);
            }

            IEnumerable<SmartWallDisplay> clean = items.Where(target => ContentIDToString.WallToString(target.WallID).Contains(Searchbar.Text));

            ContentGrid.Clear();
            ContentGrid.AddRange(clean);
        }

        private void PopulateTileEasy()
        {
            List<SmartTileDisplay> items = [];
            int count = TileLoader.TileCount;

            for (int i = 0; i < count; i++)
            {
                SmartTileDisplay e = new(i);

                /*
                e.OnMiddleClick += (_, _) =>
                {
                    e.GenCard(CardContainer);
                };
                */

                items.Add(e);
            }

            IEnumerable<SmartTileDisplay> clean = items.Where(target => ContentIDToString.TileToString(target.TileID).Contains(Searchbar.Text));

            ContentGrid.Clear();
            ContentGrid.AddRange(clean);
        }

        private void PopulateProjectileEasy()
        {
            List<SmartProjectileDisplay> items = [];
            int count = ProjectileLoader.ProjectileCount;

            for (int i = 0; i < count; i++)
            {
                SmartProjectileDisplay e = new(i);

                e.OnMiddleClick += (_, _) =>
                {
                    e.GenCard(CardContainer);
                };

                items.Add(e);
            }

            IEnumerable<SmartProjectileDisplay> clean = items.Where(target => ContentIDToString.ProjectileIdToString(target.ProjType).Contains(Searchbar.Text));

            ContentGrid.Clear();
            ContentGrid.AddRange(clean);
        }

        private void PopulateBuffEasy()
        {
            List<SmartBuffDisplay> items = [];
            int count = BuffLoader.BuffCount;

            for (int i = 1; i < count; i++)
            {
                SmartBuffDisplay e = new(i);

                e.OnMiddleClick += (_, _) =>
                {
                    e.GenCard(CardContainer);
                };

                items.Add(e);
            }

            IEnumerable<SmartBuffDisplay> clean = items.Where(target => ContentIDToString.BuffIdToString(target.buffID).Contains(Searchbar.Text));

            ContentGrid.Clear();
            ContentGrid.AddRange(clean);
        }

        private SmartNPCDisplay[] PopulateNPC()
        {
            List<SmartNPCDisplay> items = [];
            int count = NPCLoader.NPCCount;

            for (int i = 0; i < count; i++)
            {
                SmartNPCDisplay e = new(i);

                e.OnMiddleClick += (_, _) =>
                {
                    e.GenCard(CardContainer);
                };

                items.Add(e);
            }

            IEnumerable<SmartNPCDisplay> clean = items.Where(target => ContentIDToString.NPCIdToString(target.NPCType).Contains(Searchbar.Text));

            return [.. clean];
        }

        private ItemDisplay[] PopulateItems(bool insert = true)
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
                    SmartNPCDisplay[] displays = PopulateNPC();

                    ContentGrid.Clear();
                    ContentGrid.AddRange(displays);
                }
            });
        }

        private void PopulateItemsAsync()
        {
            //Task.Run(() =>
            {
                //lock (GridContainer)
                {
                    var display = PopulateItems(false);

                    ContentGrid.Clear();
                    ContentGrid.AddRange(display);
                }
            }//);
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