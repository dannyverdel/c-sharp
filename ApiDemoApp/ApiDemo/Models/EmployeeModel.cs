using System;
using Bogus;

namespace ApiDemo.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Function { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }

    public class EmployeeModelFaker : Faker<EmployeeModel>
    {
        public EmployeeModelFaker() {
            int id = 1;

            string[] functions = { "Junior Software Engineer", "Senior Software Engineer", "Project manager", "Medior Software Engineer" };

            UseSeed(Random.Shared.Next(1, 1000001))
                .RuleFor(x => x.Id, _ => id++)
                .RuleFor(x => x.Name, emf => emf.Person.FullName)
                .RuleFor(x => x.Function, _ => functions[Random.Shared.Next(0, functions.Length)])
                .RuleFor(x => x.BirthDate, emf => emf.Person.DateOfBirth);
        }
    }
}

