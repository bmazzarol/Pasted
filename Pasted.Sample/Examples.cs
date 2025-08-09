namespace Pasted.Sample;

public static partial class Examples
{
    #region Example1

    // only embed enabled, no other options set
    const string file1 = EmbeddedFiles.SampleText;

    #endregion

    #region Example2

    // all options set, with an original file name of SampleText2.md
    // namespace = Test
    // class_name = Files
    // name = ExampleMdFile1
    // public = true
    private const string file2 = Test.Files.ExampleMdFile1;

    #endregion
}
