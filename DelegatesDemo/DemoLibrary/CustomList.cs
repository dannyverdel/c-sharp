using Newtonsoft.Json;

namespace DemoLibrary
{
    public class CustomList<T>
    {
        private T[] _items;

        public CustomList() {
            _items = new T[0];
        }

        public void Add(T item) => _items = _items.Concat(new T[] { item }).ToArray();
        public void Remove(T item) {
            List<T> temp = _items.ToList();
            temp.RemoveAt(FindIndex(item));
            _items = temp.ToArray();
        }

        public void RemoveAt(int index) {
            List<T> temp = _items.ToList();
            temp.RemoveAt(index);
            _items = temp.ToArray();
        }

        public int Count() => _items.Length;

        public T? this[int index] => index >= Count() ? default : _items[index];

        public CustomList<T> Where(Func<T, bool> comparer) {
            CustomList<T> output = new CustomList<T>();
            foreach ( T item in _items )
                if ( comparer(item) ) output.Add(item);

            return output;
        }

        public T? FindOne(Func<T, bool> comparer) {
            foreach ( T item in _items )
                if ( comparer(item) ) return item;

            return default(T);
        }

        public int FindIndex(T item) {
            int index = -1;
            for ( int i = 0; i < _items.Length; i++ ) {
                if ( EqualityComparer<T>.Default.Equals(_items[i], item) )
                    index = i;
            }

            return index;
        }

        public T First() => _items[0];
        public T? FirstOrDefault() => Count() == 0 ? default : this[0];

        public T Last() => _items[Count() - 1];
        public T? LastOrDefault() => Count() == 0 ? default : _items[Count() - 1];

        public decimal Sum(Func<T, decimal> comparer) {
            decimal sum = 0;
            foreach ( T item in _items )
                sum += comparer(item);

            return sum;
        }
        public int Sum(Func<T, int> comparer) {
            int sum = 0;
            foreach ( T item in _items )
                sum += comparer(item);

            return sum;
        }
        public float Sum(Func<T, float> comparer) {
            float sum = 0;
            foreach ( T item in _items )
                sum += comparer(item);

            return sum;
        }
        public double Sum(Func<T, double> comparer) {
            double sum = 0;
            foreach ( T item in _items )
                sum += comparer(item);

            return sum;
        }

        public override string ToString() => JsonConvert.SerializeObject(_items, Formatting.None,
        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        public string ToString(bool indented) => JsonConvert.SerializeObject(_items, indented ? Formatting.Indented : Formatting.None,
        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}

