using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using MagicxorAnalyzer.CSharp.Helpers;

namespace MagicxorAnalyzer.CSharp.Models;

public readonly struct LocalizableRule : System.IEquatable<LocalizableRule>
{
    public LocalizableRule(
        ushort ruleNumber,
        Category category,
        DiagnosticSeverity diagnosticSeverity,
        bool isEnabledByDefault,
        string titleResource,
        string formatResource,
        string descriptionResource)
    {
        DiagnosticId = DiagnosticIdHelper.GetDiagnosticId(category, ruleNumber);
        Title = new LocalizableResourceString(titleResource, Resources.ResourceManager, typeof(Resources));
        Format = new LocalizableResourceString(formatResource, Resources.ResourceManager, typeof(Resources));
        Description = new LocalizableResourceString(descriptionResource, Resources.ResourceManager, typeof(Resources));

        Descriptor = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            Format,
            category.Name,
            diagnosticSeverity,
            isEnabledByDefault: isEnabledByDefault,
            description: Description);
    }

    public string DiagnosticId { get; }
    public LocalizableString Title { get; }
    public LocalizableString Format { get; }
    public LocalizableString Description { get; }
    public DiagnosticDescriptor Descriptor { get; }

    public static bool operator ==(LocalizableRule left, LocalizableRule right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(LocalizableRule left, LocalizableRule right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return obj is LocalizableRule rule && Equals(rule);
    }

    public bool Equals(LocalizableRule other)
    {
        return DiagnosticId == other.DiagnosticId
               && Title.Equals(other.Title)
               && Format.Equals(other.Format)
               && Description.Equals(other.Description)
               && EqualityComparer<DiagnosticDescriptor>.Default.Equals(Descriptor, other.Descriptor);
    }

    public override int GetHashCode()
    {
        return (DiagnosticId, Title, Format, Description, Descriptor).GetHashCode();
    }
}
