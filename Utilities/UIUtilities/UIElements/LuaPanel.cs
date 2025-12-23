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
        public UIElement InfoPanel, InfoDisplay, Execute;
        public UIText Text;
        public string LuaFileLocation;

        public LuaPanel(string path)
        {
            LuaFileLocation = path;

            Texture = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/QuestionMark").Value;
            Execute = new();
            InfoPanel = new();
            InfoDisplay = new();
            Text = this.InsertText(Path.GetFileNameWithoutExtension(LuaFileLocation));

            InfoDisplay.Width.Set(0, .5f);
            Execute.Width.Set(0, .5f);
            InfoDisplay.Height.Percent = Execute.Height.Percent = 1;
            Execute.VAlign = InfoDisplay.VAlign = .5f;
            InfoDisplay.HAlign = 0f;
            Execute.HAlign = 1f;

            Execute.InsertText($"[i:{ItemID.PaperAirplaneA}]");
            Execute.OnLeftClick += DoLua;

            Width.Set(0, 1f);
            Height.Set(30, 0);

            InfoPanel.Append(InfoDisplay);
            InfoPanel.Append(Execute);
            InfoPanel.MaxWidth.Set(60, 0);
            InfoPanel.Height.Set(0, 1);
            InfoPanel.VAlign = 0.5f;
            InfoPanel.HAlign = 1;

            Append(InfoPanel);
        }

        public override void OnInitialize()
        {
            Texture = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/QuestionMark").Value;
            Execute = new();
            InfoPanel = new();
            InfoDisplay = new();
            Text = this.InsertText(Path.GetFileNameWithoutExtension(LuaFileLocation));

            InfoDisplay.Width.Set(0, .5f);
            Execute.Width.Set(0, .5f);
            InfoDisplay.Height.Percent = Execute.Height.Percent = 1;
            Execute.VAlign = InfoDisplay.VAlign = .5f;
            InfoDisplay.HAlign = 0f;
            Execute.HAlign = 1f;

            Execute.InsertText($"[i:{ItemID.PaperAirplaneA}]");
            Execute.OnLeftClick += DoLua;

            Width.Set(0, 1f);
            Height.Set(30, 0);

            InfoPanel.Append(InfoDisplay);
            InfoPanel.Append(Execute);
            InfoPanel.MaxWidth.Set(60, 0);
            InfoPanel.Height.Set(0, 1);
            InfoPanel.VAlign = 0.5f;
            InfoPanel.HAlign = 1;

            Append(InfoPanel);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Execute?.SetUIBase("Execute Lua");

            if (InfoDisplay != null && Texture != null)
            {
                spriteBatch.Draw(Texture, InfoDisplay.GetDimensions().Position() - new Vector2(Texture.Width / 2, Texture.Height / 2), Colors.CoinGold);
            }
            else
            {
                Texture = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/QuestionMark").Value;
            }

            base.Draw(spriteBatch);
        }

        public void DoLua(UIEvent evt, UIElement element)
        {
            try
            {
                string FileContents = File.ReadAllText(LuaFileLocation);

                Task.Run( () => AlienBloxUtility.RunLuaAsync(FileContents));
            }
            catch (FileNotFoundException)
            {
                Parent.RemoveChild(this);
            }
        }
    }
}