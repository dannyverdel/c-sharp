// See https://aka.ms/new-console-template for more information
using CSharpDemos.ClassLibrary;

IInvokeMethod im = new InvokeBogus();

im.InvokeMethod();
Console.ReadLine();