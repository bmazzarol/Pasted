<!-- markdownlint-disable MD033 MD041 -->
<div align="center">

<img src="images/glue-icon.png" alt="Pasted" width="150px"/>

# Pasted

---

[![Nuget](https://img.shields.io/nuget/v/Pasted)](https://www.nuget.org/packages/Pasted/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Pasted&metric=coverage)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Pasted)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Pasted&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Pasted)
[![CD Build](https://github.com/bmazzarol/Pasted/actions/workflows/cd-build.yml/badge.svg)](https://github.com/bmazzarol/Pasted/actions/workflows/cd-build.yml)
[![Check Markdown](https://github.com/bmazzarol/Pasted/actions/workflows/check-markdown.yml/badge.svg)](https://github.com/bmazzarol/Pasted/actions/workflows/check-markdown.yml)

Compile time text file embedding for C#

---

</div>

## Why?

Pasted is a simple .NET source generator that allows for compile time embedding
of text files into your application. It is particularly useful for embedding
resources such as HTML, CSS, JavaScript, or any other text-based files directly
into your code. It works well for source generators and applications
that are compiled ahead of time (AOT).

It comes with a MSBuild task that is used to select the files to embed, the
source generator then generates a static class with constants for each file.
