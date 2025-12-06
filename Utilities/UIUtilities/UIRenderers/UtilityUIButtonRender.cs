using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIRenderers
{
    [Autoload(Side = ModSide.Client)]
    public class UtilityUIButtonRender : ModSystem
    {
        internal ShowUtilityMenuButton Element;

        private UserInterface _element;

        public override void Load()
        {
            Element = new ShowUtilityMenuButton();
            Element.Activate();
            _element = new UserInterface();
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
                    "AlienBloxUtility: AlienBlox's Utilities Activation Button",
                    delegate
                    {
                        _element.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}