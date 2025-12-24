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
                            Lua((string)args[1]);
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