// See https://aka.ms/new-console-template for more information
using CSharpDemos.ClassLibrary;

var im = new CSharpDemos.ClassLibrary.DatabaseDemo.InvokeDemo();

await im.InvokeMethod();
Console.ReadLine();