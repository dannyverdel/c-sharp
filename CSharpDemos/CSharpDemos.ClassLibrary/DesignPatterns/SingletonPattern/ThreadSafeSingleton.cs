using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.SingletonPattern.ThreadSafeSingleton
{
	public class InvokeThreadSafeSingleton : IInvokeMethod
	{
        public void InvokeMethod()
        {
            for (int i = 0; i < 10; i++)
                MemoryCache.Create();
        }
    }

    public class MemoryCache
    {
        private static int _i = 0;
        private static MemoryCache? _instance;
        private static object _cache_lock = new object();
        private MemoryCache() => $"Created {_i++}".Dump();
        public static MemoryCache Create()
        {
            if (_instance == null)
                lock (_cache_lock)
                    if (_instance == null)
                        _instance = new MemoryCache();

            return _instance;
        }
    }
}

