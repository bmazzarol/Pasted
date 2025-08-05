using System.Text;
using Microsoft.CodeAnalysis;

namespace Pasted;

/// <summary>
/// Source generator for Pasted embedded files.
/// </summary>
#pragma warning disable RS1038
[Generator]
#pragma warning restore RS1038
public sealed class SourceGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Collect all additional files (these are provided by MSBuild)
        var embeddedFiles = context.AdditionalTextsProvider.Select((file, _) => file).Collect();

        context.RegisterSourceOutput(
            embeddedFiles,
            (spc, files) =>
            {
                // TODO: Get the root namespace from analyzer config options
                var ns = "Pasted";

                foreach (var file in files)
                {
                    var name = Path.GetFileNameWithoutExtension(file.Path)
                        .Replace('-', '_')
                        .Replace('.', '_');
                    var content = file.GetText(spc.CancellationToken)
                        ?.ToString()
                        .Replace("\"", "\\\"");
                    var sb = new StringBuilder();
                    sb.Append("namespace ").Append(ns).AppendLine(" {");
                    sb.AppendLine("    internal static partial class EmbeddedFiles {");
                    sb.Append("        public const string ")
                        .Append(name)
                        .Append(" = @\"")
                        .Append(content)
                        .AppendLine("\";");
                    sb.AppendLine("    }");
                    sb.AppendLine("}");
                    spc.AddSource($"EmbeddedFiles.{name}.g.cs", sb.ToString());
                }
            }
        );
    }
}
