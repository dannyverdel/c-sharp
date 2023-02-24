namespace DemoLibrary.Tests;

public class ExamplesTests
{
    [Fact]
    public void ExampleLoadTextFileValidNameShouldWork() {
        string actual = Example.ExampleLoadTextFile("This is a valid file name.");
        Assert.True(actual.Length > 0);
    }

    [Fact]
    public void ExampleLoadTextFileValidNameShouldFail() {
        Assert.Throws<ArgumentException>("file", () => Example.ExampleLoadTextFile(""));
    }
}

