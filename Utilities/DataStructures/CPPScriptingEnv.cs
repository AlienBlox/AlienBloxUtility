using AlienBloxUtility.Utilities.Abstracts;
using CLanguage;
using CLanguage.Compiler;
using CLanguage.Interpreter;
using CLanguage.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AlienBloxUtility.Utilities.DataStructures
{
    public class CPPScriptingEnv : ScriptingEnvironment<CCompiler>
    {
        public CancellationTokenSource MainToken;

        public List<(string, Action)> CFunctions;

        public CPPScriptingEnv()
        {
            MainToken = new();
            Env = new();
        }

        public override void Run(string code)
        {
            try
            {
                var program = CCompiler.Compile(code);

                Task task = Task.Run(() =>
                {
                    if (MainToken.IsCancellationRequested)
                    {
                        MainToken.ThrowIfCancellationRequested();
                    }

                    try
                    {
                        var interpreter = new CInterpreter(program);

                        interpreter.Reset("main");
                        interpreter.Run();
                    }
                    catch (Exception e)
                    {
                        AlienBloxUtility.LuaStdout.WriteLine($"{e.GetType().Name}: {e.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                AlienBloxUtility.LuaStdout.WriteLine($"{ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}