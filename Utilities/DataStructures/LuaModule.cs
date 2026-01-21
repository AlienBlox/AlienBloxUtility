using System.Collections.Generic;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.DataStructures
{
    public class LuaModule : ILoadable
    {
        public static List<LuaModule> AllModules;

        public string Name => GetType().Name;

        public Mod Mod {  get; private set; }

        public virtual void OnLoad()
        {

        }

        public virtual void OnUnload()
        {

        }

        public void Load(Mod mod)
        {
            AllModules ??= [];
            Mod = mod;

            OnLoad();

            AllModules.Add(this);
        }

        public void Unload()
        {
            OnUnload();

            AllModules?.Remove(this);
        }
    }
}