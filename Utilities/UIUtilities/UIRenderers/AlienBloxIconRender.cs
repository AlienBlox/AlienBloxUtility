using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIRenderers
{
    public class AlienBloxIconRender : ModSystem
    {
        public static AlienBloxIconRender Instance { get; private set; }

        internal AlienBloxIcon Element;

        private UserInterface _element;

        public AlienBloxIconRender()
        {
            Instance = this;
        }

        ~AlienBloxIconRender()
        {
            Instance = null;
        }

        public override void Load()
        {
            Element = new();
            Element.Activate();
            _element = new();
            _element.SetState(Element);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _element?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "AlienBloxUtility: Mouse Icon Override",
                    delegate
                    {
                        _element.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public static void SetIcon(int ico)
        {
            Instance.Element.SetItem(ico);
        }
    }
}
