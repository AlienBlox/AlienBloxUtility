using AlienBloxUtility.Utilities.DataStructures;
using CLanguage.Compiler;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
        public static CPPScriptingEnv SharedCPP;

        public static CCompiler CCompiler => SharedCPP.Env;

        public static void CPP(string CPlusPlus) => SharedCPP.Run(CPlusPlus);
    }
}