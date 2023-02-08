// See https://aka.ms/new-console-template for more information
using CSharpDemos.ClassLibrary;

IInvokeMethod im = new CSharpDemos.ClassLibrary.DesignPatterns.SingletonPattern.SyncAccessProblem.InvokeSyncAccessProblem();

im.InvokeMethod();
Console.ReadLine();