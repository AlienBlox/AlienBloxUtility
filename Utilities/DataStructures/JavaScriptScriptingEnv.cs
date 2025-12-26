using AlienBloxUtility.Utilities.Abstracts;
using Jint;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.DataStructures
{
    public class JavaScriptScriptingEnv : ScriptingEnvironment<Engine>
    {
        public static List<JavaScriptScriptingEnv> Envs;

        private JavaScriptScriptingEnv()
        {
            Env = AlienBloxUtility.EngineCreate();
        }

        public override void Run(string code)
        {
            RunJavaScript(code, new());

            base.Run(code);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);

            Env = null;
        }

        public Task RunJavaScript(string code, object engineLock)
        {
            return Task.Run(async () => RunAsync(code, engineLock));
        }

        /// <summary>
        /// Runs a Jint script asynchronously and safely.
        /// </summary>
        /// <param name="jsCode">JavaScript code to execute</param>
        /// <returns>Task for awaiting completion</returns>
        public Task RunAsync(string jsCode, object engineLock)
        {
            return Task.Run(() =>
            {
                lock (engineLock)
                {
                    Env.Execute(jsCode);
                }
            });
        }

        /// <summary>
        /// Evaluates a JavaScript expression asynchronously and returns the result.
        /// </summary>
        /// <param name="jsCode">JavaScript expression</param>
        /// <returns>Result as object</returns>
        public Task<object> EvaluateAsync(string jsCode, object engineLock)
        {
            return Task.Run(() =>
            {
                lock (engineLock)
                {
                    return Env.Evaluate(jsCode).ToObject();
                }
            });
        }

        /// <summary>
        /// Safely invoke a JS function asynchronously.
        /// </summary>
        /// <param name="functionName">Function name</param>
        /// <param name="args">Arguments</param>
        /// <returns>Result as object</returns>
        public Task<object> InvokeAsync(string functionName, object engineLock, params object[] args)
        {
            return Task.Run(() =>
            {
                lock (engineLock)
                {
                    return Env.Invoke(functionName, args).ToObject();
                }
            });
        }

        /// <summary>
        /// Expose a C# value or object to JavaScript
        /// </summary>
        public void SetValue(string name, object engineLock, object value)
        {
            lock (engineLock)
            {
                Env.SetValue(name, value);
            }
        }

        /// <summary>
        /// Creates a new lua scripting environment
        /// </summary>
        /// <returns>The environment to give</returns>
        /// <exception cref="Exception"></exception>
        public static JavaScriptScriptingEnv Create()
        {
            var env = new JavaScriptScriptingEnv();

            Envs ??= [];

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (Envs.Count > AlienBloxUtilityConfig.Instance.JavaScriptMaxCount)
                {
                    throw new Exception("JS count overfilled.");
                }
            }
            else
            {
                if (Envs.Count > AlienBloxUtilityServerConfig.Instance.JavaScriptMaxCount)
                {
                    throw new Exception("JS count overfilled.");
                }
            }

            Envs.Add(env);

            return env;
        }
    }
}