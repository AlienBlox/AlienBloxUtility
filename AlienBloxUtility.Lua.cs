using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Neo.IronLua;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
            if (LuaEnv != null)
            {
                LuaEnv[name] = func;
            }
        }

        public static void Deregister(string name)
        {
            if (LuaEnv != null)
            {
                LuaEnv[name] = null;
            }
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

        public static Task<LuaResult> RunLuaAsync(string code, CancellationTokenSource tokenSource)
        {
            var token = tokenSource.Token;

            ConHostRender.Write(Language.GetTextValue("Mods.AlienBloxUtility.UI.ScriptStart"));

            return Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                LuaResult result = null;

                try
                {
                    result = LuaEnv.DoChunk(code, "chunk");

                    ConHostRender.Write(Language.GetTextValue("Mods.AlienBloxUtility.UI.ScriptEnd"));
                }
                catch (Exception ex)
                {
                    // Handle Lua runtime errors
                    Console.WriteLine("Lua error: " + ex.Message);
                    ConHostRender.Write("Lua error: " + ex.Message);
                }

                token.ThrowIfCancellationRequested();
                return result;
            }, token);
        }

        public static Task<LuaResult> RunLuaAsync(string code, CancellationTokenSource tokenSource, params KeyValuePair<string, object>[] objects)
        {
            var token = tokenSource.Token;

            return Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                LuaResult result = null;

                try
                {
                    result = LuaEnv.DoChunk(code, "chunk", objects);
                }
                catch (Exception ex)
                {
                    // Handle Lua runtime errors
                    ConHostRender.Write("Lua error: " + ex.Message);
                    Console.WriteLine("Lua error: " + ex.Message);
                }

                token.ThrowIfCancellationRequested();
                return result;
            }, token);
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

        public static void Lua(string lua)
        {
            RunLuaAsync(lua, GetToken());
        }

        public static async void TestRun(string code)
        {
            try
            {
                // Run asynchronously on a background thread
                LuaResult result = await RunLuaAsync(code, GetToken());

                // Now we are back on the main thread continuation
                if (result != null)
                {
                    var table = result[2] as LuaTable;
                    if (table != null)
                    {
                        foreach (var kv in table)
                            Instance.Logger.Debug($"Results: {kv} = {table[kv]}");
                    }
                }
            }
            catch (Exception E)
            {
                Instance.Logger.Warn(E.Message);
                throw new("Aw Crap!");
            }
        }
    }
}