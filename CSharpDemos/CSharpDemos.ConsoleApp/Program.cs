// See https://aka.ms/new-console-template for more information
using CSharpDemos.ClassLibrary;

IInvokeMethod im = new CSharpDemos.ClassLibrary.DesignPatterns.BuilderPattern.Builder.WrongBuilder.InvokeWrongBuilder();

im.InvokeMethod();
Console.ReadLine();