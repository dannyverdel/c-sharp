using System;
using Newtonsoft.Json;

namespace CSharpDemos.ClassLibrary
{
    public static class StringExtension
    {
        public static string? Dump<T>(this T obj)
        {
            if (obj is null) return null;
            Console.WriteLine(obj.ToString());

            return obj.ToString();
        }
    }
}

