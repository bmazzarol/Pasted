# Pasted

![Pasted](images/glue-icon.png)

**Text files, baked into the binary.**

A source generator that embeds the contents of text files into C# at compile time, for source generators and AOT apps where there is no filesystem at runtime.

```shell
dotnet add package Pasted
```

## Why?

Pasted is a simple .NET source generator that allows for compile time embedding
of text files into your application. It is particularly useful for embedding
resources such as HTML, CSS, JavaScript, or any other text-based files directly
into your code. It works well for source generators and applications
that are compiled ahead of time (AOT).
