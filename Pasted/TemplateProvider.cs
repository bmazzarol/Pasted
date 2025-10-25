using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Pasted;

[ExcludeFromCodeCoverage]
internal static class TemplateProvider
{
    internal static void WriteTemplate(this StringBuilder builder, RenderModel model)
    {
        builder.Append("namespace ");
        builder.Append(model.Namespace);
        builder.Append(
            @";

/// <summary>
/// Compiled embedded files
/// </summary>
"
        );
        builder.Append(model.Visibility);
        builder.Append(" static partial class ");
        builder.Append(model.ClassName);
        builder.Append(
            @"
{
    /// <summary>
    /// "
        );
        builder.Append(model.Path);
        builder.Append(
            @" embedded file
    /// </summary>
    public const string "
        );
        builder.Append(model.FieldName);
        builder.Append(@" = @""");
        builder.Append(model.Content);
        builder.Append(
            @""";
}
"
        );
    }
}
