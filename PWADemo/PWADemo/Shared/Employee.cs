using System;
using Bogus;

namespace PWADemo.Shared
{
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
	}

	public class EmployeeFaker : Faker<Employee>
	{
		public EmployeeFaker()
		{
			int id = 1;

			Random rnd = new Random();

			UseSeed(rnd.Next(1000))
				.RuleFor(x => x.Id, _ => id++)
				.RuleFor(x => x.Name, ef => ef.Person.FullName)
				.RuleFor(x => x.Email, ef => ef.Person.Email)
				.RuleFor(x => x.Phone, ef => ef.Person.Phone);
		}
	}
}

