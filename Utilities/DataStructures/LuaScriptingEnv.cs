using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace AlienBloxUtility.Utilities.DataStructures
{
    /// <summary>
    /// A new lua environment.
    /// </summary>
    public class LuaScriptingEnv : ScriptingEnvironment<Lua>
    {
        public static List<LuaScriptingEnv> Envs;

        /// <summary>
        /// The connected Lua environment
        /// </summary>
        public LuaGlobal LuaEnv;

        private LuaScriptingEnv()
        {
            Env = new();
            LuaEnv = Env.CreateEnvironment();
        }

        public override void Run(string code)
        {
            Task.Run(() => RunLuaAsync(code, AlienBloxUtility.GetToken()));

            base.Run(code);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);

            LuaEnv = null;
            Env = null;
        }

        public string MakeWhileLoopsSafe(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return code;

            // Handle while loops
            string whilePattern = @"\bwhile\b\s*(.*?)\s*do";
            code = Regex.Replace(code, whilePattern, m =>
            {
                string condition = m.Groups[1].Value;
                return $"while {condition} do check_timeout()";
            }, RegexOptions.Singleline);

            // Handle repeat ... until loops
            string repeatPattern = @"\brepeat\b";
            code = Regex.Replace(code, repeatPattern, "repeat check_timeout()");

            return code;
        }

        public void InjectCheckTimeout(LuaScriptContext context)
        {
            // Assign a per-script check_timeout function
            LuaEnv["check_timeout"] = (Action)(() =>
            {
                var elapsed = DateTime.UtcNow - context.StartTime;
                if (elapsed.TotalMilliseconds > context.MaxMilliseconds)
                    throw new Exception("Lua loop timeout exceeded!");
            });
        }

        /// <summary>
        /// Registers a function to the global Lua system
        /// </summary>
        /// <param name="name">The name of the function</param>
        /// <param name="func">The function to register</param>
        public void RegisterFunc(string name, Delegate func)
        {
            if (LuaEnv != null)
            {
                LuaEnv[name] = func;
            }
        }

        public Task<LuaResult> RunLuaAsync(string code, CancellationTokenSource tokenSource, params KeyValuePair<string, object>[] objects)
        {
            tokenSource ??= AlienBloxUtility.GlobalCts;

            var token = tokenSource.Token;

            return Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                LuaResult result = null;

                try
                {
                    InjectCheckTimeout(new(5000));

                    result = LuaEnv.DoChunk(MakeWhileLoopsSafe(code), "chunk", objects);

                    if (Main.netMode != NetmodeID.Server)
                        ConHostRender.Write(Language.GetTextValue("Mods.AlienBloxUtility.UI.ScriptEnd"));
                }
                catch (Exception ex)
                {
                    // Handle Lua runtime errors
                    if (Main.netMode != NetmodeID.Server)
                        ConHostRender.Write("Lua error: " + ex.Message);
                    Console.WriteLine("Lua error: " + ex.Message);
                }

                token.ThrowIfCancellationRequested();
                return result;
            }, token);
        }

        public void Deregister(string name)
        {
            if (LuaEnv != null)
            {
                LuaEnv[name] = null;
            }
        }

        /// <summary>
        /// Creates a new lua scripting environment
        /// </summary>
        /// <returns>The environment to give</returns>
        /// <exception cref="Exception"></exception>
        public static LuaScriptingEnv Create()
        {
            var env = new LuaScriptingEnv();
            
            Envs ??= [];

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (Envs.Count > AlienBloxUtilityConfig.Instance.LuaMaxCount)
                {
                    throw new Exception("lua count overloaded.");
                }
            }
            else
            {
                if (Envs.Count > AlienBloxUtilityServerConfig.Instance.LuaMaxCount)
                {
                    throw new Exception("lua count overloaded.");
                }
            }

            Envs.Add(env);

            return env;
        }
    }
}