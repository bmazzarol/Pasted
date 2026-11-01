# Getting Started

Add the embedded files to your project, for example:

```xml
<ItemGroup>
    <AdditionalFiles Include="wwwroot\index.html" />
    <AdditionalFiles Include="wwwroot\css\site.css" />
    <AdditionalFiles Include="wwwroot\js\site.js" />
</ItemGroup>
```

Then in an `.editorconfig` file add the following property,

```editorconfig
[wwwroot/index.html]
embed = true

[wwwroot/css/site.css]
embed = true

[wwwroot/js/site.js]
embed = true
```

Then finally add the `Pasted` source generator to your project:

```xml
<ItemGroup>
    <PackageReference Include="Pasted" Version="x.x.x">
        <PrivateAssets>all</PrivateAssets>
        <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
</ItemGroup>
```

The generator emits a static class with one constant per embedded file,

```c#
namespace Pasted;

/// <summary>
/// Compiled embedded files
/// </summary>
internal static partial class EmbeddedFiles
{
    /// <summary>
    /// index.html embedded file
    /// </summary>
    public const string index = "<!doctype html>...";
}
```

so the contents are available as compile time constants,

```c#
const string html = EmbeddedFiles.index;
```

The namespace and static property name can be configured in the
`.editorconfig` file.

## Configuration Options

<!-- markdownlint-disable MD013 -->
| Option     | Type    | Default                                 | Description                                                                                       |
|------------|---------|-----------------------------------------|---------------------------------------------------------------------------------------------------|
| embed      | boolean | false                                   | Enables embedding this file into the generated class. Files with `embed = true` will be included. |
| namespace  | string  | Pasted                                  | Overrides the namespace used for the generated static class.                                      |
| class_name | string  | EmbeddedFiles                           | Overrides the name of the generated static class that contains file constants.                    |
| name       | string  | Derived from file name (sans extension) | Overrides the constant field name for the file; by default uses the file name without extension.  |
| public     | boolean | false                                   | Controls visibility of generated class and its fields; `true` makes them public, else internal.   |
