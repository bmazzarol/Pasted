using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Pasted.Tests;

public class SharedTests
{
    [Fact(DisplayName = "Text files can be embedded in a project")]
    public async Task Case1()
    {
        var driver = BuildDriver(
            [
                new TestAdditionalFile("test.txt", SourceText.From("test")),
                new TestAdditionalFile("test.ignored", SourceText.From("test")),
            ]
        );

        await Verify(driver);
    }

    private static GeneratorDriver BuildDriver(ImmutableArray<AdditionalText> additionalTexts)
    {
        var compilation = CSharpCompilation.Create(
            "name",
            [],
            [],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
        var generator = new EmbeddedFilesSourceGenerator();
        var driver = CSharpGeneratorDriver
            .Create(generator)
            .AddAdditionalTexts(additionalTexts)
            .WithUpdatedAnalyzerConfigOptions(new TestAnalyzerConfigOptionsProvider());
        return driver.RunGenerators(compilation);
    }

    private sealed class TestAdditionalFile(string path, SourceText text) : AdditionalText
    {
        public override string Path => path;

        public override SourceText GetText(CancellationToken cancellationToken = default)
        {
            return text;
        }
    }

    private class TestAnalyzerConfigOptionsProvider : AnalyzerConfigOptionsProvider
    {
        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree)
        {
            throw new NotSupportedException();
        }

        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile)
        {
            return new TestAnalyzerConfigOptions(textFile);
        }

        private class TestAnalyzerConfigOptions(AdditionalText textFile) : AnalyzerConfigOptions
        {
            public override bool TryGetValue(string key, [NotNullWhen(true)] out string? value)
            {
                if (
                    !textFile.Path.Contains(".ignored", StringComparison.Ordinal)
                    && string.Equals(key, "embed", StringComparison.Ordinal)
                )
                {
                    value = "true";
                    return true;
                }

                if (string.Equals(key, "replace_quotes", StringComparison.Ordinal))
                {
                    value = "true";
                    return true;
                }

                value = null;
                return false;
            }
        }

        public override AnalyzerConfigOptions GlobalOptions => throw new NotSupportedException();
    }
}
