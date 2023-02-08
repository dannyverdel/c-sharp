using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.SingletonPattern.SyncProblemSolution2
{
    public class InvokeSyncProblemSolution2 : IInvokeMethod
    {
        public void InvokeMethod()
        {
            int size = 20;
            Task[] tasks = new Task[size];
            for (int i = 0; i < size; i++)
                tasks[i] = Task.Run(() => MemoryCache.Create());

            Task.WaitAll(tasks);
            //MemoryCache.Create();
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
                lock(_cache_lock)
                    if(_instance == null)
                        return _instance = new MemoryCache();

            return _instance;
        }
    }
}

