# Pasted

![Pasted](images/glue-icon.png)

**Text files, baked into the binary.**

A source generator that turns text files into string constants at compile time, for source generators and AOT apps where there is no filesystem at runtime.

```shell
dotnet add package Pasted
```

## Why?

A source generator cannot read a file from disk while it is running, and an AOT
binary has no assembly directory to read from at runtime. Pasted puts the file
contents into the compilation as constants, so the data is there when the code
runs.

Given `test.txt` containing `test`, the generator emits,

```c#
namespace Pasted;

/// <summary>
/// Compiled embedded files
/// </summary>
internal static partial class EmbeddedFiles
{
    /// <summary>
    /// test.txt embedded file
    /// </summary>
    public const string test = @"test";
}
```

Because the value is a compile-time constant it can be used inside another
source generator, which is how [Cutout](https://github.com/bmazzarol/Cutout)
embeds its own templates.

## What it does not do

* It only embeds text. Binary files are not supported
* Files are not embedded unless `embed = true` is set for them in
  `.editorconfig`
* It does not watch files. Changing an embedded file requires a rebuild

## See also

* [Getting Started](getting-started.md) - wire up the generator and the
  `.editorconfig` options
