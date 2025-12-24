using AlienBloxUtility.Utilities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            Task.Run(() => RunLuaAsync((string)args[1], GetToken()));
                            break;
                        case "RegisterLua":
                            LuaEnv.Add(args[1], args[2]);
                            break;
                        case "RegisterCommand":
                            ModCommandHelper.Commands.TryAdd((string)args[1], (Action<string[]>)args[2]);
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