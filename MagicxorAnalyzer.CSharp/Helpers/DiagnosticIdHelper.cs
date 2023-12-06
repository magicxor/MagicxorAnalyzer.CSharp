using MagicxorAnalyzer.CSharp.Models;

namespace MagicxorAnalyzer.CSharp.Helpers;

public static class DiagnosticIdHelper
{
    public static string GetDiagnosticId(Category category, ushort ruleNumber)
    {
        return $"MX{category.ShortName}{ruleNumber:D4}";
    }
}
