using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.IteratorPattern
{
    public class InvokeIteratorPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            var aggregate = new ConcreteAggregate<string>();
            aggregate.Add("Item 1");
            aggregate.Add("Item 2");
            aggregate.Add("Item 3");

            var iterator = aggregate.CreateIterator();
            while(!iterator.IsDone())
            {
            iterator.CurrentItem().Dump();
            iterator.Next();
            }
        }
    }

    interface IIterator<T>
    {
        T First();
        bool IsDone();
        T Next();
        T CurrentItem();
    }

    interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }

    class ConcreteIterator<T> : IIterator<T>
    {
        private readonly List<T> _items;
        private int _current;

        public ConcreteIterator(List<T> items)
        {
            _items = items;
            _current = 0;
        }

        public T First() => _items[0];

        public bool IsDone() => _current >= _items.Count;

        public T Next() => _current >= _items.Count ? _items[_current] : _items[_current++];

        public T CurrentItem() => _items[_current];
    }

    class ConcreteAggregate<T> : IAggregate<T>
    {
        private readonly List<T> _items = new List<T>();

        public IIterator<T> CreateIterator()
        {
            return new ConcreteIterator<T>(_items);
        }

        public void Add(T item)
        {
            _items.Add(item);
        }
    }
}

