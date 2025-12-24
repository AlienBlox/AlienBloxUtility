using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Neo.IronLua;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Terraria.Localization;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
        public static class CentralizedLua
        {
            /// <summary>
            /// Rewrites all while loops to insert a per-script timeout check.
            /// </summary>
            private static string MakeWhileLoopsSafe(string code)
            {
                string pattern = @"\bwhile\b\s*(.*?)\s*do";

                string safeCode = Regex.Replace(code, pattern, m =>
                {
                    string condition = m.Groups[1].Value;
                    return $"while {condition} do check_timeout()";
                }, RegexOptions.Singleline);

                return safeCode;
            }

            public static void RunSafeScriptAsync(string code, int max = 1000, CancellationToken cancellationToken = default)
            {
                Task.Run( () => RunSafeScriptAsync(code, max, cancellationToken), cancellationToken);
            }

            /// <summary>
            /// Runs Lua code safely using the centralized environment.
            /// </summary>
            public static async Task RunSafeScriptAsync(string code, int MaxTime = 1000, CancellationToken cancellationToken = default, params KeyValuePair<string, object>[] objects)
            {
                // Each script gets its own startTime
                var startTime = DateTime.UtcNow;

                // Inject a per-script timeout function
                LuaEnv["check_timeout"] = (Action)(() =>
                {
                    if (cancellationToken.IsCancellationRequested)
                        throw new OperationCanceledException("Script cancelled");

                    var elapsed = DateTime.UtcNow - startTime;
                    if (elapsed.TotalMilliseconds > MaxTime)
                        throw new Exception("Lua loop timeout exceeded!");
                });

                string safeCode = MakeWhileLoopsSafe(code);

                // Run script in a Task to allow async execution
                await Task.Run(() =>
                {
                    LuaEnv.DoChunk(safeCode, "script", objects);
                }, cancellationToken);
            }
        }

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
            tokenSource ??= GlobalCts;

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
            tokenSource ??= GlobalCts;

            var token = tokenSource.Token;

            return Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                LuaResult result = null;

                try
                {
                    result = LuaEnv.DoChunk(code, "chunk", objects);

                    ConHostRender.Write(Language.GetTextValue("Mods.AlienBloxUtility.UI.ScriptEnd"));
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

        public static Task<LuaResult> Lua(string lua, CancellationTokenSource tokenSource = null)
        {
            tokenSource ??= GlobalCts;

            return RunLuaAsync(lua, tokenSource);
        }

        public static void Lua(string lua, int timeOutCount)
        {
            var tsk = Lua(lua);

            if (!tsk.Wait(timeOutCount))
            {
                tsk.Dispose(); // signal cancellation
                Console.WriteLine("Timed out");
            }
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