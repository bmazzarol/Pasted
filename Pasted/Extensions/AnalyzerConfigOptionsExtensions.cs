using Microsoft.CodeAnalysis.Diagnostics;

namespace Pasted.Extensions;

internal static class AnalyzerConfigOptionsExtensions
{
    internal static bool IsEnabled(this AnalyzerConfigOptions options, string key)
    {
        return options.TryGetValue(key, out var value)
            && bool.TryParse(value, out var enabled)
            && enabled;
    }
}
