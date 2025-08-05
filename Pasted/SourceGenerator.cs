using System.CodeDom.Compiler;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Cutout;

/// <summary>
/// Source generator for Pasted embedded files.
/// </summary>
#pragma warning disable RS1038
[Generator]
#pragma warning restore RS1038
public sealed partial class SourceGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context) { }
}
