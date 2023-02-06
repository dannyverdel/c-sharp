using System;
using System.Text;
namespace CSharpDemos.ClassLibrary.DesignPatterns.BuilderPattern.Builder
{
    public class InvokeBuilderPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            //QueryBuilder builder = new QueryBuilder();
            //FormBodyBuilder builder = new FormBodyBuilder();
            //HttpHeaderBuilder builder = new HttpHeaderBuilder();
            DictBuilder builder = new DictBuilder();

            ConstructionProcess(builder);
            builder.Build().Dump();
        }

        public void ConstructionProcess(IKeyValueCollectionBuilder builder)
        {
            builder.Add("make", "lada")
                .Add("colour", "red")
                .Add("year", 1990.ToString());
        }
    }

    public interface IKeyValueCollectionBuilder
    {
        IKeyValueCollectionBuilder Add(string key, string value);
    }

    public class QueryBuilder : IKeyValueCollectionBuilder
    {
        private StringBuilder _query_string_builder = new StringBuilder();

        public IKeyValueCollectionBuilder Add(string key, string value)
        {
            _query_string_builder.Append(_query_string_builder.Length == 0 ? "?" : "&");
            _query_string_builder.Append(key);
            _query_string_builder.Append('=');
            _query_string_builder.Append(Uri.EscapeDataString(value));
            return this;
        }

        public string Build()
        {
            return _query_string_builder.ToString();
        }
    }

    public class FormBodyBuilder : IKeyValueCollectionBuilder
    {
        private StringBuilder _form_body_builder = new StringBuilder();

        public IKeyValueCollectionBuilder Add(string key, string value)
        {
            _form_body_builder.Append(key);
            _form_body_builder.Append('=');
            _form_body_builder.Append(value);
            _form_body_builder.AppendLine();
            return this;
        }

        public string Build()
        {
            return _form_body_builder.ToString();
        }
    }

    public class HttpHeaderBuilder : IKeyValueCollectionBuilder
    {
        private StringBuilder _http_header_builder = new StringBuilder();

        public IKeyValueCollectionBuilder Add(string key, string value)
        {
            _http_header_builder.Append(key);
            _http_header_builder.Append(": ");
            _http_header_builder.Append(value);
            _http_header_builder.AppendLine();
            return this;
        }

        public string Build()
        {
            return _http_header_builder.ToString();
        }
    }

    public class DictBuilder : IKeyValueCollectionBuilder
    {
        private Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        public IKeyValueCollectionBuilder Add(string key, string value)
        {
            _dictionary[key] = value;
            return this;
        }

        public Dictionary<string, string> Build()
        {
            return _dictionary;
        }
    }
}

