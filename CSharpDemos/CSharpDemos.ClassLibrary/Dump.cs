using System;
using Newtonsoft.Json;

namespace CSharpDemos.ClassLibrary
{
    public static class StringExtension
    {
        public static string Dump<T>(this T obj, bool indent = true)
        {
            string res = JsonConvert.SerializeObject(obj, indent ? Formatting.Indented : Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            Console.WriteLine(res);

            return res;
        }
    }
}

