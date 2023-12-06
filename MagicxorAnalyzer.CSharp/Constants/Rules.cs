using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MagicxorAnalyzer.CSharp.Models;

namespace MagicxorAnalyzer.CSharp.Constants;

public static class Rules
{
    public static readonly LocalizableRule PrimaryConstructorsNotAllowedRule = new(
        ruleNumber: 1,
        Categories.CodeStyle,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        nameof(Resources.PrimaryConstructorsAreNotAllowedTitle),
        nameof(Resources.PrimaryConstructorsAreNotAllowedMessageFormat),
        nameof(Resources.PrimaryConstructorsAreNotAllowedDescription));

    public static readonly IReadOnlyDictionary<string, LocalizableRule> AllRules = new Dictionary<string, LocalizableRule>
    {
        { PrimaryConstructorsNotAllowedRule.DiagnosticId, PrimaryConstructorsNotAllowedRule },
    };

    public static readonly ImmutableArray<DiagnosticDescriptor> AllDescriptors = AllRules
        .Values
        .Select(x => x.Descriptor)
        .ToImmutableArray();
}
