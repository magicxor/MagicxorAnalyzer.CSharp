using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using MagicxorAnalyzer.CSharp.Constants;

namespace MagicxorAnalyzer.CSharp;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class MagicxorAnalyzerCSharpAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Rules.AllDescriptors;

    public override void Initialize(AnalysisContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
        context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
    }

    private static void ApplyTypeRules(SymbolAnalysisContext context, INamedTypeSymbol namedTypeSymbol)
    {
        if (context.IsGeneratedCode)
        {
            return;
        }

        var typeMembers = namedTypeSymbol.GetMembers();

        foreach (var member in typeMembers)
        {
            if (member is IMethodSymbol { MethodKind: MethodKind.Constructor }
                && member.DeclaringSyntaxReferences.Length > 0
                /* the following two cover C# 12's class/struct primary constructors */
                && member.DeclaringSyntaxReferences[0].GetSyntax() is ClassDeclarationSyntax { ParameterList.Parameters.Count: > 0 }
                    or StructDeclarationSyntax { ParameterList.Parameters.Count: > 0 })
            {
                var diagnostic = Diagnostic.Create(Rules.PrimaryConstructorsNotAllowedRule.Descriptor, member.Locations[0], member.Name);
                context.ReportDiagnostic(diagnostic);
                break;
            }
        }
    }

    private static void AnalyzeSymbol(SymbolAnalysisContext context)
    {
        if (context.Symbol is not INamedTypeSymbol namedTypeSymbol)
        {
            return;
        }

        if (namedTypeSymbol.IsType)
        {
            ApplyTypeRules(context, namedTypeSymbol);
        }
    }
}
