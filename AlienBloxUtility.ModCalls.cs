using AlienBloxUtility.Utilities.Commands;
using AlienBloxUtility.Utilities.Helpers;
using System;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
        public override object Call(params object[] args)
        {
            object Msg = args[0];

            try
            {
                if (Msg is string S)
                {
                    switch (S)
                    {
                        case "Lua":
                            return Task.Run(() => RunLuaAsync((string)args[1], GetToken()));
                        case "RegisterLua":
                            LuaEnv.Add(args[1], args[2]);
                            break;
                        case "RegisterCommand":
                            ModCommandHelper.Commands.TryAdd((string)args[1], (Action<string[]>)args[2]);
                            break;
                        case "DoJavaScript":
                            return RunJavaScript((string)args[1]);
                        case "AddToJSEnv":
                            SetValue((string)args[1], args[2]);
                            break;
                        case "RegisterDoc":
                            DocumentationStorage.RegisterEntry((Mod)args[1], (string)args[2], (string)args[3], (bool)args[4], (bool)args[5], (bool)args[6], (bool)args[7]);
                            break;
                    }
                }
            }
            catch (Exception E)
            {
                return E;
            }

            return base.Call(args);
        }
    }
}