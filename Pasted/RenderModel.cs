namespace Pasted;

internal sealed record RenderModel(
    string Path,
    string Namespace,
    string ClassName,
    string FieldName,
    string Content,
    string Visibility
);
