using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class LuaPanel : UIPanel
    {
        public Texture2D Texture;
        public UIElement InfoPanel, InfoDisplay;
        public UIText Text;
        public string LuaFileLocation;

        public LuaPanel(string path)
        {
            LuaFileLocation = path;

            Texture = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/QuestionMark").Value;
            InfoPanel = new();
            InfoDisplay = new();
            Text = this.InsertText(Path.GetFileNameWithoutExtension(LuaFileLocation));

            InfoDisplay.Width.Set(0, .5f);
            InfoDisplay.Height.Percent = 1;
            InfoDisplay.HAlign = 0f;
            Width.Set(0, 1f);
            Height.Set(30, 0);

            InfoPanel.Append(InfoDisplay);
            InfoPanel.MaxWidth.Set(60, 0);
            InfoPanel.Height.Set(0, 1);
            InfoPanel.VAlign = 0.5f;
            InfoPanel.HAlign = 1;

            Append(InfoPanel);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            this.SetUIBase("Execute Lua");

            base.DrawSelf(spriteBatch);
        }

        public void DoLua(UIEvent evt, UIElement element)
        {
            try
            {
                string FileContents = File.ReadAllText(LuaFileLocation);

                Task.Run( () => AlienBloxUtility.RunLuaSafe(FileContents, out _, out _));
            }
            catch (FileNotFoundException)
            {
                Parent.RemoveChild(this);
            }
        }
    }
}