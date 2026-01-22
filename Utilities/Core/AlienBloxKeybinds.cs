using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class AlienBloxKeybinds : ModSystem
    {
        public static ModKeybind SudoKeybind { get; private set; }

        public static ModKeybind ToggleAdvancedDebugging { get; private set; }

        //public static ModKeybind UseTileTool { get; private set; }

        public override void Load()
        {
            SudoKeybind = KeybindLoader.RegisterKeybind(Mod, "Sudo", Keys.NumPad1);
            ToggleAdvancedDebugging = KeybindLoader.RegisterKeybind(Mod, "ToggleAdvancedDebugging", Keys.NumPad2);
            //UseTileTool = KeybindLoader.RegisterKeybind(Mod, "UseTileTool", Keys.NumPad3);
        }
    }
}