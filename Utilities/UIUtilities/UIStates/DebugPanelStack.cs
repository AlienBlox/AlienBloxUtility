using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DebugPanelStack : UIState
    {
        private UIElement _myPanel;

        private List<ButtonIcon> _buttons;

        private bool SetData = false;

        public override void OnInitialize()
        {
            _myPanel = new UIElement();

            _buttons = CreateButtonRow(_myPanel, 6, 50, 50, 10, "Mods.AlienBloxUtility.Buttons.ConsoleButton", ItemID.IronPickaxe, Color.White);

            Append(_myPanel);
        }

        private List<ButtonIcon> CreateButtonRow(UIElement parent, int buttonCount, int buttonWidth, int buttonHeight, int spacing, string key, int itemID, Color selectorColor)
        {
            List<ButtonIcon> buttonList = new List<ButtonIcon>();

            parent.Width.Set(buttonWidth * buttonCount + (spacing * buttonCount), 0f);
            parent.Height.Set(buttonHeight, 0f);
            parent.Left.Set((_myPanel.Width.Pixels / 4) - (spacing * buttonCount) / 2 - buttonWidth + spacing / 2, 0);
            parent.HAlign = .5f;
            parent.VAlign = 0.05f;

            for (int i = 0; i < buttonCount; i++)
            {
                // Create a new button
                ButtonIcon button = new(key, itemID, selectorColor);
                button.Width.Set(buttonWidth, 0f);
                button.Height.Set(buttonHeight, 0f);
                button.Left.Set(i * (buttonWidth + spacing), 0f); // Space buttons horizontally
                button.Top.Set(0, 0f); // Keep the buttons aligned at the top

                button.OnMouseOver += button.OnHover;
                button.OnLeftClick += button.OnToggle;

                // Add click event for the button
                int index = i; // Capture index for the click event

                // Add the button to the parent panel
                parent.Append(button);

                // Add the button to the list to return later
                buttonList.Add(button);
            }

            // Return the list of created buttons
            return buttonList;
        }

        public override void Update(GameTime gameTime)
        {
            if (DebugUtilityList.PacketSpyEnabled && Main.netMode != NetmodeID.MultiplayerClient)
            {
                DebugUtilityList.PacketSpyEnabled = false;
                _buttons[3].Toggle = false;
            }

            if (!SetData)
            {
                _buttons[0].SetStats(ItemID.GravediggerShovel, "Mods.AlienBloxUtility.Buttons.ConsoleButton", Color.Green);
                _buttons[0].OnLeftClick += ToggleConsole;

                _buttons[1].SetStats(ItemID.Zenith, "Mods.AlienBloxUtility.Buttons.StatsButton", Color.Purple);
                _buttons[1].OnLeftClick += ToggleStatsButton;

                _buttons[2].SetStats(ItemID.DirtBlock, "Mods.AlienBloxUtility.Buttons.DecompilerButton", Color.Brown);
                _buttons[2].OnLeftClick += ToggleDecompilerButton;

                _buttons[3].SetStats(ItemID.PaperAirplaneA, "Mods.AlienBloxUtility.Buttons.PacketSpyButton", Color.MintCream);
                _buttons[3].OnLeftClick += TogglePacketSpyButton;

                _buttons[4].SetStats(ItemID.FlaskofNanites, "Mods.AlienBloxUtility.Buttons.MilkerButton", Color.LightBlue);
                _buttons[4].OnLeftClick += ToggleStatsButton;

                _buttons[5].SetStats(ItemID.AngelStatue, "Mods.AlienBloxUtility.Buttons.ExtrasButton", Color.DarkSlateGray);
                _buttons[5].OnLeftClick += ToggleExtrasMenuButton;

                SetData = true;
            }
            

            base.Update(gameTime);
        }

        public void ToggleConsole(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.ConsoleWindowEnabled = _buttons[0].Toggle;
        }

        public void ToggleStatsButton(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.StatsMenuEnabled = _buttons[1].Toggle;
        }

        public void ToggleDecompilerButton(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.DecompilerMenuEnabled = _buttons[2].Toggle;
        }

        public void TogglePacketSpyButton(UIMouseEvent Evt, UIElement Element)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                _buttons[3].Toggle = false;
                DebugUtilityList.PacketSpyEnabled = false;
                Main.NewText(Language.GetTextValue("Mods.AlienBloxUtility.Messages.PacketSpy.NotOnMP"));

                return;
            }

            DebugUtilityList.PacketSpyEnabled = _buttons[3].Toggle;

            if (DebugUtilityList.PacketSpyEnabled)
            {
                Main.NewText(Language.GetTextValue("Mods.AlienBloxUtility.Messages.PacketSpy.MsgBegin"));
            }
            else
            {
                Main.NewText(Language.GetTextValue("Mods.AlienBloxUtility.Messages.PacketSpy.MsgEnd"));
            }
        }

        public void ToggleSystemStatsButton(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.MilkerEnabled = _buttons[4].Toggle;
        }

        public void ToggleExtrasMenuButton(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.ExtrasMenuEnabled = _buttons[5].Toggle;
        }
    }
}