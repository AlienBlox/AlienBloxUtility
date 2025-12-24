using Jint;
using System;
using System.Threading.Tasks;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
        public static Task RunJavaScript(string code)
        {
            return Task.Run(async () => RunAsync(code));
        }

        /// <summary>
        /// Creates a new JavaScript engine
        /// </summary>
        /// <returns></returns>
        public static Engine EngineCreate(int maxRecursion = 100, int maxStatements = 1000, int timeOut = 100)
        {
            return new Engine(options =>
            {
                options
                    .LimitRecursion(maxRecursion)
                    .MaxStatements(maxStatements)
                    .TimeoutInterval(TimeSpan.FromMilliseconds(timeOut));
            });
        }

        /// <summary>
        /// Runs a Jint script asynchronously and safely.
        /// </summary>
        /// <param name="jsCode">JavaScript code to execute</param>
        /// <returns>Task for awaiting completion</returns>
        public static Task RunAsync(string jsCode)
        {
            return Task.Run(() =>
            {
                lock (engineLock)
                {
                    JSEngine.Execute(jsCode);
                }
            });
        }

        /// <summary>
        /// Evaluates a JavaScript expression asynchronously and returns the result.
        /// </summary>
        /// <param name="jsCode">JavaScript expression</param>
        /// <returns>Result as object</returns>
        public static Task<object> EvaluateAsync(string jsCode)
        {
            return Task.Run(() =>
            {
                lock (engineLock)
                {
                    return JSEngine.Evaluate(jsCode).ToObject();
                }
            });
        }

        /// <summary>
        /// Safely invoke a JS function asynchronously.
        /// </summary>
        /// <param name="functionName">Function name</param>
        /// <param name="args">Arguments</param>
        /// <returns>Result as object</returns>
        public static Task<object> InvokeAsync(string functionName, params object[] args)
        {
            return Task.Run(() =>
            {
                lock (engineLock)
                {
                    return JSEngine.Invoke(functionName, args).ToObject();
                }
            });
        }

        /// <summary>
        /// Expose a C# value or object to JavaScript
        /// </summary>
        public static void SetValue(string name, object value)
        {
            lock (engineLock)
            {
                JSEngine.SetValue(name, value);
            }
        }
    }
}
