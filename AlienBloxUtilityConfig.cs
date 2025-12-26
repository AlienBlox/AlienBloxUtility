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

        [DefaultValue(false)]
        [BackgroundColor(25, 50, 25)]
        public bool GeneralDebugMessages;

        [Header("Lua")]
        [DefaultValue(true)]
        [ReloadRequired]
        [BackgroundColor(25, 50, 25)]
        public bool Sandboxed;

        [DefaultValue(true)]
        [BackgroundColor(25, 50, 25)]
        public bool Noisy;

        [DefaultValue(10)]
        [ReloadRequired]
        [BackgroundColor(25, 50, 25)]
        public int LuaMaxCount;

        [Header("JavaScript")]
        [ReloadRequired]
        [BackgroundColor(25, 50, 25)]
        public int JavaScriptMaxCount;

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

        [Header("Debugger")]
        [DefaultValue(false)]
        [BackgroundColor(25, 50, 25)]
        public bool ClearCache;

        [Header("Lua")]
        [DefaultValue(true)]
        [ReloadRequired]
        [BackgroundColor(25, 50, 25)]
        public bool Sandboxed;

        [DefaultValue(10)]
        [ReloadRequired]
        [BackgroundColor(25, 50, 25)]
        public int LuaMaxCount;

        [Header("JavaScript")]
        [ReloadRequired]
        [BackgroundColor(25, 50, 25)]
        public int JavaScriptMaxCount;

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