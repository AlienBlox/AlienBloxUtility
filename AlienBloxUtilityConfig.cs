using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace AlienBloxUtility
{
    [BackgroundColor(50, 100, 50, 128)]
    public class AlienBloxUtilityConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static AlienBloxUtilityConfig Instance;

        [Header("Debugger")]
        [DefaultValue(true)]
        [BackgroundColor(25, 50, 25)]
        public bool DecompilerMessages;

        [BackgroundColor(25, 50, 25)]
        public bool GeneralDebugMessages;

        public override void OnLoaded()
        {
            Instance = this;
        }
    }

    [BackgroundColor(50, 100, 50, 128)]
    public class AlienBloxUtilityServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static AlienBloxUtilityServerConfig Instance;

        public override void OnLoaded()
        {
            Instance = this;
        }

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
        {
            return false;
        }
    }
}