using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIRenderers
{
    [Autoload(Side = ModSide.Client)]
    public class SpawningMenuRender : ModSystem
    {
        public static bool SpawnMenuEnabled;

        internal SpawningMenu Element;

        private UserInterface _element;

        public override void Load()
        {
            Element = new();
            Element.Activate();
            _element = new UserInterface();
            _element.SetState(Element);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (SpawnMenuEnabled)
            {
                if (_element?.CurrentState == null)
                {
                    _element?.SetState(Element);
                }

                _element?.Update(gameTime);
            }
            else
            {
                _element?.SetState(null);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "AlienBloxUtility: Spawn Menu",
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