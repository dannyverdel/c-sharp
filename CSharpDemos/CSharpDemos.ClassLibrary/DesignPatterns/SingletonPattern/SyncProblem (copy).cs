﻿using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.SingletonPattern.SyncProblemSolution
{
    public class InvokeSyncProblemSolution : IInvokeMethod
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
        private static MemoryCache _instance;
        static MemoryCache() => _instance = new MemoryCache();
        private MemoryCache() => $"Created {_i++}".Dump();
        public static MemoryCache Create() => _instance;
    }
}

