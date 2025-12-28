using AlienBloxUtility.Utilities.DataStructures;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Neo.IronLua;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static List<CancellationTokenSource> CentralTokenStorage;
        public static ConcurrentQueue<Action> MainThreadQueue;
#pragma warning restore CA2211 // Non-constant fields should not be visible}

        public static CancellationTokenSource GetToken()
        {
            var token = new CancellationTokenSource();

            CentralTokenStorage.Add(token);

            return token;
        }

        /// <summary>
        /// Registers a function to the global Lua system
        /// </summary>
        /// <param name="name">The name of the function</param>
        /// <param name="func">The function to register</param>
        public static void RegisterFunc(string name, Delegate func)
        {
            LuaUnifiedEnv.RegisterFunc(name, func);
        }

        public static void Deregister(string name)
        {
            LuaUnifiedEnv.Deregister(name);
        }

        public static void QueueMainThreadAction(Action action)
        {
            MainThreadQueue.Enqueue(action);
        }

        public static void ProcessQueue()
        {
            while (MainThreadQueue.TryDequeue(out var action))
            {
                action.Invoke();
            }
        }

        public static Task<LuaResult> RunLuaAsync(string code, CancellationTokenSource tokenSource, params KeyValuePair<string, object>[] objects)
        {
            return LuaUnifiedEnv.RunLuaAsync(code, tokenSource, objects);
        }

        public static void CancelAll()
        {
            try
            {
                foreach (var token in CentralTokenStorage)
                {
                    token.Cancel();
                    token.Dispose();
                }

                CentralTokenStorage.Clear();
            }
            catch
            {
                ConHostRender.Write("Can't cancel lua tasks.");
            }
        }

        public static void CancelGlobal()
        {
            GlobalCts?.Cancel();
            GlobalCts = new();
        }

        public static Task<LuaResult> Lua(string lua, CancellationTokenSource tokenSource = null)
        {
            return LuaUnifiedEnv.RunLuaAsync(lua, tokenSource);
        }
    }
}