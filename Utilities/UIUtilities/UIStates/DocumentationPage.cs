using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DocumentationPage : UIState
    {
        public DraggablePanelMinimum BackingElement;
        public UIPanel BackingPanel, SideBar, InnerPanel, SearchbarBacking, ChartBacker;
        public UIList ScrollChart, BackingChart;
        public UIScrollbar Scrollbar, BackingScroll;
        public UITextBoxImproved TextBox;
        public ButtonIcon ButtonLua, ButtonCSharp, ButtonCore, ButtonJS, EnterButton;

        public override void OnInitialize()
        {
            ButtonCore = new("Mods.AlienBloxUtility.UI.Documentation.Core", ItemID.IronAxe, Color.Red);
            ButtonLua = new("Mods.AlienBloxUtility.UI.Documentation.Lua", ItemID.MoonCharm, Color.Blue);
            ButtonJS = new("Mods.AlienBloxUtility.UI.Documentation.JavaScript", ItemID.PaperAirplaneA, Color.Yellow);
            ButtonCSharp = new("Mods.AlienBloxUtility.UI.Documentation.CSharp", ItemID.Zenith, Color.Gray);
            EnterButton = new("Mods.AlienBloxUtility.UI.Documentation.Search", ItemID.PaperAirplaneA, Color.Gray);

            TextBox = new("");
            BackingElement = new();
            BackingPanel = new();
            SideBar = new();
            InnerPanel = new();
            SearchbarBacking = new();
            Scrollbar = new();
            BackingScroll = new();
            ChartBacker = new();
            ScrollChart = [ButtonCore, ButtonLua, ButtonJS, ButtonCSharp];
            BackingChart = [];

            SearchbarBacking.Width.Set(0, 1);
            SearchbarBacking.Height.Set(0, .1f);
            SearchbarBacking.VAlign = 0;
            SearchbarBacking.HAlign = .5f;
            SearchbarBacking.BackgroundColor = new(0, 0, 0, 0);
            SearchbarBacking.BorderColor.A = 0;

            ButtonJS.Width.Set(0, 1);
            ButtonJS.Height.Set(60, 0);
            ButtonCore.Width.Set(0, 1);
            ButtonCore.Height.Set(60, 0);
            ButtonLua.Width.Set(0, 1);
            ButtonLua.Height.Set(60, 0);
            ButtonCSharp.Width.Set(0, 1);
            ButtonCSharp.Height.Set(60, 0);

            ScrollChart.Width.Set(0, 1);
            ScrollChart.Height.Set(0, 1);
            ScrollChart.VAlign = ScrollChart.HAlign = .5f;

            Scrollbar.VAlign = .5f;

            BackingElement.Left.Set(Main.screenWidth / 2, 0);
            BackingElement.Top.Set(Main.screenHeight / 2, 0);
            BackingElement.Width.Set(600, 0);
            BackingElement.Height.Set(750, 0);
            BackingElement.SetPadding(15);

            SideBar.BackgroundColor = new(0, 0, 0, 0);
            SideBar.BorderColor = new(0, 0, 0, 0);
            SideBar.SetPadding(5);
            SideBar.Width.Set(0, .2f);
            SideBar.Height.Set(0, 1f);
            SideBar.VAlign = 0.5f;
            SideBar.HAlign = 0f;

            BackingPanel.Width.Set(0, .8f);
            BackingPanel.Height.Set(0, 1f);
            BackingPanel.VAlign = 0.5f;
            BackingPanel.HAlign = 1;
            BackingPanel.BackgroundColor = new(100, 65, 0);

            ChartBacker.Width.Set(0, 1f);
            ChartBacker.Height.Set(0, .9f);
            ChartBacker.VAlign = 1;
            ChartBacker.HAlign = .5f;
            ChartBacker.BackgroundColor = new(0, 0, 0, 0);
            ChartBacker.BorderColor = new(0,0, 0, 0);

            InnerPanel.Width.Set(0, 1f);
            InnerPanel.Height.Set(0, 1f);
            InnerPanel.VAlign = InnerPanel.HAlign = .5f;
            InnerPanel.BackgroundColor = new(255, 245, 160);

            ScrollChart.ManualSortMethod = (_) => { };
            BackingChart.ManualSortMethod = (_) => { };
            BackingChart.Width.Set(0, 1f);
            BackingChart.Height.Set(0, 1f);
            BackingChart.VAlign = 1;
            BackingChart.HAlign = .5f;
            BackingChart.Append(BackingScroll);
            BackingChart.SetScrollbar(BackingScroll);
            BackingScroll.VAlign = .5f;

            EnterButton.HAlign = 1;
            EnterButton.VAlign = 0.5f;
            EnterButton.Height.Set(0, 1);
            EnterButton.Width.Set(0, .1f);
            EnterButton.MaxWidth.Set(50, 0);

            TextBox.Width.Set(0, 1);
            TextBox.Height.Set(0, 1);
            TextBox.VAlign = 0.5f;
            TextBox.HAlign = 0.5f;
            TextBox.Append(EnterButton);

            SearchbarBacking.Append(TextBox);
            InnerPanel.Append(SearchbarBacking);
            ChartBacker.Append(BackingChart);
            InnerPanel.Append(ChartBacker);
            ScrollChart.Append(Scrollbar);
            ScrollChart.SetScrollbar(Scrollbar);
            SideBar.Append(ScrollChart);
            BackingPanel.Append(InnerPanel);
            BackingElement.Append(SideBar);
            BackingElement.Append(BackingPanel);
            Append(BackingElement);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (ButtonCore.ContainsPoint(Main.MouseScreen))
            {
                Main.hoverItemName = Language.GetTextValue("Mods.AlienBloxUtility.UI.Documentation.Core");
            }

            if (ButtonLua.ContainsPoint(Main.MouseScreen))
            {
                Main.hoverItemName = Language.GetTextValue("Mods.AlienBloxUtility.UI.Documentation.Lua");
            }

            if (ButtonJS.ContainsPoint(Main.MouseScreen))
            {
                Main.hoverItemName = Language.GetTextValue("Mods.AlienBloxUtility.UI.Documentation.JavaScript");
            }

            if (ButtonCSharp.ContainsPoint(Main.MouseScreen))
            {
                Main.hoverItemName = Language.GetTextValue("Mods.AlienBloxUtility.UI.Documentation.CSharp");
            }
        }
    }
}