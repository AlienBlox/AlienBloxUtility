using AlienBloxUtility.Utilities.Core;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DebugUIState : UIState
    {
        public override void OnInitialize()
        {
            var UIPanel = new UIPanel();

            var panel2 = new UIPanel();

            var panel3 = new UIPanel();

            panel3.Width.Set(300, 0);
            panel3.Height.Set(300, 0);
            panel3.VAlign = UIPanel.HAlign = .5f;

            panel2.Width = UIPanel.Height = new(0, 1);
            panel2.VAlign = UIPanel.HAlign = .5f;

            UIPanel.Width = UIPanel.Height = new(0, 1);
            UIPanel.VAlign = UIPanel.HAlign = .5f;
            UIPanel.InsertList();

            panel2.Append(UIPanel);

            panel3.Append(panel2);

            Append(panel3);
        }
    }
}