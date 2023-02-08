using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.SingletonPattern.BadExample
{
    public class InvokeBadExample : IInvokeMethod
    {
        public void InvokeMethod()
        {
            MemoryCache.Create();
            MemoryCache.Create();
            MemoryCache.Create();
        }
    }

    public class MemoryCache
    {
        private static MemoryCache? _instance;

        private MemoryCache() => "Created".Dump();

        public static MemoryCache Create() => _instance ??= new MemoryCache();
    }
}

