using System.Diagnostics.CodeAnalysis;
using System.Text;
using Cutout;

namespace Pasted;

[ExcludeFromCodeCoverage]
internal static partial class TemplateProvider
{
    private const string Template = """
        namespace {{model.Namespace}};

        /// <summary>
        /// Compiled embedded files
        /// </summary>
        {{model.Visibility}} static partial class {{model.ClassName}}
        {
            /// <summary>
            /// {{model.Path}} embedded file
            /// </summary>
            public const string {{model.FieldName}} = @""{{model.Content}}"";
        }

        """;

    [Template(Template)]
    internal static partial void WriteTemplate(this StringBuilder builder, RenderModel model);
}
