using System;

namespace AlienBloxUtility.Utilities.Abstracts
{
    /// <summary>
    /// A scripting environment used for handling scripts.
    /// </summary>
    /// <typeparam name="T">The object to connect.</typeparam>
    public abstract class ScriptingEnvironment<T> : IDisposable where T : class
    {
        public T Env; //{ get; private set; }

        public event Action<T, string> RunCallback;

        public virtual void Run(string code)
        {
            RunCallback?.Invoke(Env, code);
        }

        public virtual void Dispose()
        {
            //GC.SuppressFinalize(this);
        }
    }
}