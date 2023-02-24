using System;
namespace DemoLibrary;

public static class Example
{
    public static string ExampleLoadTextFile(string file) {
        if ( file.Length < 10 )
            throw new ArgumentException("The file name was too short", nameof(file));

        return "The file was correctly loaded.";
    }
}

