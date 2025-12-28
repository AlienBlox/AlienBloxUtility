using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace AlienBloxAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AlienBloxAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => throw new System.NotImplementedException();

        public override void Initialize(AnalysisContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}