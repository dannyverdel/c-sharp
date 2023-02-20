using System;
using System.ComponentModel.DataAnnotations;
using Bogus;

namespace PWADemo.Shared
{
	public class Employee
	{
		public int Id { get; set; }
		[Required]
		[StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters long.")]
		public string Name { get; set; } = string.Empty;

		[Required]
		[RegularExpression(@"^([\w-\.+]+)@(([[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$", ErrorMessage = "Email must be a valid email.")]
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

