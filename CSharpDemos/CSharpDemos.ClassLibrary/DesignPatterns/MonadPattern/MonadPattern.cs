using System;
using Bogus;
namespace CSharpDemos.ClassLibrary.DesignPatterns.MonadPattern
{
    /*
	 * The Monad design pattern is a way of chaining together a series of actions in a way that preserves the context or state of those actions. 
	 * It provides a way to create a sequence of actions that can be executed in order, with each action building on the result of the previous action.
	 * 
	 * In C#, the most common example of the Monad design pattern is the LINQ query syntax. 
	 * LINQ allows you to chain together a series of queries that operate on a collection of data. 
	 * Each query in the chain takes the result of the previous query and applies a new operation to it. The context or state of the collection is preserved throughout the chain.
	 * 
	 * The LINQ query syntax is a great example of the Monad design pattern because it allows you to chain together a series of operations on a 
	 * collection in a way that preserves the state of the collection. This makes it easy to write code that is both functional and efficient.
	 */
    public class InvokeMonadPattern : IInvokeMethod
	{
		public void InvokeMethod()
		{
			List<Employee> employees = new EmployeeFaker().Generate(1000);

			employees.Where(x => x.Name == null ? false : x.Name.ToLower().Split(" ")[0] == "danny").ToList().Dump();
		}
	}

	public class Employee
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
	}

	public class EmployeeFaker : Faker<Employee>
	{
		public EmployeeFaker()
		{
            int id = 1;

            Random rnd = new Random();

			UseSeed(rnd.Next(10000))
				.RuleFor(mf => mf.Id, _ => id++)
				.RuleFor(mf => mf.Name, f => f.Person.FullName)
				.RuleFor(mf => mf.Email, f => f.Person.Email);
        }
	}
}