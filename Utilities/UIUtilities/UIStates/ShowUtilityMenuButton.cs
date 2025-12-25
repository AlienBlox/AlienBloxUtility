using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ShowUtilityMenuButton : UIState
    {
        private AlienBloxUtilityButton _UI;

        public override void OnInitialize()
        {
            _UI = new();
            _UI.Width.Set(50f, 0f);
            _UI.Height.Set(50f, 0f);
            _UI.Left.Set(10f, 0f);
            _UI.Top.Set(-10f, 0f);
            _UI.HAlign = 0f;  // Align to the right (1 = right edge of the screen)
            _UI.VAlign = 1f;  // Align to the bottom (1 = bottom edge of the screen)

            _UI.OnLeftClick += ToggleDebugPanel;
            _UI.OnRightClick += ToggleDocumentationUI;

            Append(_UI);
        }

        public static void ToggleDebugPanel(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            DebugUtilityList.DebugMenuEnabled = !DebugUtilityList.DebugMenuEnabled;
        }

        public static void ToggleDocumentationUI(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            DocumentationRender.DocumentationEnabled = !DocumentationRender.DocumentationEnabled;
        }
    }
}