using Neo.IronLua;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
        #pragma warning disable CA2211 // Non-constant fields should not be visible
        public static ConcurrentQueue<Action> MainThreadQueue;
        public static CancellationTokenSource Cts;
        #pragma warning restore CA2211 // Non-constant fields should not be visible

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

        public static Task<LuaResult> RunLuaAsync(string code, KeyValuePair<string, object>? pair = null)
        {
            var token = Cts.Token;

            return Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                LuaResult result = null;

                try
                {
                    if (pair != null)
                    {
                        result = LuaEnv.DoChunk(code, "chunk", pair.Value);
                    }
                    else
                    {
                        result = LuaEnv.DoChunk(code, "chunk");
                    }
                }
                catch (Exception ex)
                {
                    // Handle Lua runtime errors
                    Console.WriteLine("Lua error: " + ex.Message);
                }

                token.ThrowIfCancellationRequested();
                return result;
            }, token);
        }

        public static void Cancel()
        {
            Cts?.Cancel();
            Cts = new CancellationTokenSource();
        }
        
        public static void Lua(string lua)
        {
            RunLuaAsync(lua);
        }

        public static async Task<object> TryRun(string lua, KeyValuePair<string, object>? kvp = null)
        {
            var result = await RunLuaAsync(lua, kvp);

            return result;
        }

        public static async void TestRun(string code)
        {
            try
            {
                // Run asynchronously on a background thread
                LuaResult result = await RunLuaAsync(code);

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