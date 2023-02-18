using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.MonoidPattern
{
    /*
	 * The Monoid design pattern is a software design pattern that provides a way to combine two or more similar objects into a single object. 
	 * The resulting object, known as a monoid, has the same type as the individual objects, 
	 * and can be combined with other monoids using the same operation to produce another monoid of the same type.
	 * 
	 * A monoid is defined by three elements: a set of values, an associative binary operation that combines two values, 
	 * and an identity element that when combined with any value in the set returns that value unchanged.
	 * 
	 * In practice, this pattern can be used in situations where you need to aggregate or merge similar data types, 
	 * such as in parallel programming or when working with collections of data.
	 * 
	 * In this example, the Counter class has an increment method that increases the counter by 1, and an overloaded + operator that combines two counters by adding their values. 
	 * The Identity property returns a new Counter instance with an initial value of 0, which serves as the identity element for the addition operation.
	 */
    public class InvokeMonoidPattern : IInvokeMethod
	{
		public void InvokeMethod()
		{
			Counter c1 = new Counter(2);
			Counter c2 = new Counter(3);
			Counter c3 = new Counter(4);

			Counter result = c1 + c2 + c3;
			result = result * c1;
			Console.WriteLine(result);
		}
	}

	public class Counter
	{
		private int _count;
		public Counter(int initial_value = 0) => _count = initial_value;
		public void Increment() => _count++;
		public static Counter operator +(Counter c1, Counter c2) => new Counter(c1._count + c2._count);
		public static Counter operator *(Counter c1, Counter c2) => new Counter(c1._count * c2._count);
		public static Counter Identity() => new Counter();
		public override string ToString() => _count.ToString();
	}
}

