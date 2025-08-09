using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Pasted.Extensions;

namespace Pasted;

/// <summary>
/// Source generator for Pasted embedded files.
/// </summary>
#pragma warning disable RS1038
[Generator]
#pragma warning restore RS1038
public sealed class EmbeddedFilesSourceGenerator : IIncrementalGenerator
{
    private const string DefaultNamespace = "Pasted";

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var embeddedFiles = context.AdditionalTextsProvider.Select((file, _) => file).Collect();
        var configProvider = context.AnalyzerConfigOptionsProvider.Select((options, _) => options);
        var renderModelProvider = embeddedFiles
            .Combine(configProvider)
            .SelectMany(BuildRenderModel);

        context.RegisterSourceOutput(renderModelProvider, GenerateEmbeddedFileSources);
    }

    private static IEnumerable<RenderModel> BuildRenderModel(
        (ImmutableArray<AdditionalText> Files, AnalyzerConfigOptionsProvider Options) ctx,
        CancellationToken token
    )
    {
        foreach (var file in ctx.Files)
        {
            var options = ctx.Options.GetOptions(file);

            if (!options.IsEnabled(ConfigOptions.Embed))
            {
                continue;
            }

            var namespaceToUse = options.TryGetValue(ConfigOptions.Namespace, out var ns)
                ? ns
                : DefaultNamespace;
            var fieldName = options.TryGetValue(ConfigOptions.FieldName, out var n)
                ? n
                : Path.GetFileNameWithoutExtension(file.Path).Replace('-', '_').Replace('.', '_');

            var sourceText = file.GetText(token);
            var content = sourceText?.ToString() ?? string.Empty;

            var visibility = options.IsEnabled(ConfigOptions.Public) ? "public" : "internal";

            var className = options.TryGetValue(ConfigOptions.ClassName, out var c)
                ? c
                : "EmbeddedFiles";

            yield return new RenderModel(
                file.Path,
                namespaceToUse,
                className,
                fieldName,
                content,
                visibility
            );
        }
    }

    private static void GenerateEmbeddedFileSources(SourceProductionContext spc, RenderModel model)
    {
        var sb = new StringBuilder();
        sb.WriteTemplate(model);
        spc.AddSource($"{model.Namespace}.{model.ClassName}.{model.FieldName}.g.cs", sb.ToString());
    }
}
