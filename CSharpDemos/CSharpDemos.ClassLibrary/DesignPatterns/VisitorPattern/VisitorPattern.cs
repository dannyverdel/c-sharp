using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.VisitorPattern
{
    /*
     *The Visitor pattern is a design pattern used to separate an algorithm from an object structure. 
     *The idea is to define a new operation that can be applied to a collection of objects, without changing the classes of those objects. 
     *The Visitor pattern introduces a new class called a visitor, which implements the algorithm for the new operation.
     *
     *In simple terms, the Visitor pattern allows you to add new functionality to an existing class hierarchy without modifying those classes directly.
     *
     *Suppose we have a class hierarchy that represents different types of employees in a company.
     *
     *We want to add a new functionality to our class hierarchy that calculates the total salary for all employees, including any bonuses. 
     *To do this, we can use the Visitor pattern. First, we define a new interface called IVisitor that contains methods for visiting each of the different employee types.
     *
     *Next, we create a concrete visitor class called SalaryCalculatorVisitor that implements the IVisitor interface and defines the algorithm for calculating the total salary.
     *
     *Finally, we can use the SalaryCalculatorVisitor to calculate the total salary for all employees.
     *
     *In this example, the SalaryCalculatorVisitor is the algorithm that is applied to the collection of employees. 
     *By using the Visitor pattern, we were able to add new functionality to our class hierarchy without modifying the Employee, Manager, or Developer classes directly.
     */

    public class InvokeVisitorPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            var employees = new List<Employee>
            {
                new Manager { Name = "John", Salary = 100000, Bonus = 20000 },
                new Developer { Name = "Jane", Salary = 80000, HasCodeAccess = true },
                new Developer { Name = "Bob", Salary = 70000, HasCodeAccess = false }
            };

            SalaryCalculatorVisitor calculator = new SalaryCalculatorVisitor();
            foreach (Employee employee in employees)
                employee.Accept(calculator);

            Console.WriteLine($"Total salary: {calculator.TotalSalary}");

            ListAllNamesVisitor list_all_names = new ListAllNamesVisitor();
            foreach (Employee employee in employees)
                employee.Accept(list_all_names);

            list_all_names.AllEmployeeNames.Dump();
        }
    }

    public abstract class Employee
    {
        public string? Name { get; set; }
        public int Salary { get; set; }

        public abstract void Accept(IVisitor visitor);
    }

    public class Manager : Employee
    {
        public int Bonus { get; set; }

        public override void Accept(IVisitor visitor) => visitor.VisitManager(this);
    }

    public class Developer : Employee
    {
        public bool HasCodeAccess { get; set; }

        public override void Accept(IVisitor visitor) => visitor.VisitDeveloper(this);
    }

    public interface IVisitor
    {
        void VisitManager(Manager manager);
        void VisitDeveloper(Developer developer);
    }

    public class SalaryCalculatorVisitor : IVisitor
    {
        public int TotalSalary { get; private set; }

        public void VisitManager(Manager manager) => TotalSalary += manager.Salary + manager.Bonus;
        public void VisitDeveloper(Developer developer) => TotalSalary += developer.Salary;
    }

    public class ListAllNamesVisitor : IVisitor
    {
        public List<string> AllEmployeeNames = new List<string>();
        public void VisitManager(Manager manager) => AllEmployeeNames.Add(manager.Name ?? "");
        public void VisitDeveloper(Developer developer) => AllEmployeeNames.Add(developer.Name ?? "");
    }
}

