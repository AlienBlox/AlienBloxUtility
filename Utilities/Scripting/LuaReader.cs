using Neo.IronLua;
using System;
using System.Reflection;

namespace AlienBloxUtility.Utilities.Scripting
{
    public static class LuaReader
    {
        public static void AddToLua<T>(LuaGlobal lua, Type typeScan, T instance)
        {
            try
            {
                foreach (var function in typeScan.GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    var luaFunc = new LuaMethod(null, function);

                    lua[typeScan.Name + '.' + function.Name] = luaFunc;
                }

                foreach (var element in typeScan.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    lua[typeScan.Name + '.' + element.Name] = element;
                }

                lua[typeScan.Name + ".instance"] = instance;
            }
            catch
            {

            }
        }
    }
}