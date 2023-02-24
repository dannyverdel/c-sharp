using Newtonsoft.Json;

namespace DemoLibrary;

public static class Object
{
    public static string Dump<T>(this T obj, bool indent = true) {
        string res = JsonConvert.SerializeObject(obj, indent ? Formatting.Indented : Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        Console.WriteLine(res);

        return res;
    }

    public static bool IsNullEmptyOrWhiteSpace(this string str) {
        if ( string.IsNullOrEmpty(str) ) return true;
        if ( string.IsNullOrWhiteSpace(str) ) return true;
        return false;
    }
}

