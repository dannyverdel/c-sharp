using DemoLibrary.Models;
using DemoLibrary;

Console.WriteLine(String.Format("{0} + {1} = {2}", 10, 15, Calculator.Add(10, 15)));
Console.WriteLine(String.Format("{0} - {1} = {2}", 10, 15, Calculator.Subtract(10, 15)));
Console.WriteLine(String.Format("{0} * {1} = {2}", 10, 15, Calculator.Multiply(10, 15)));
Console.WriteLine(String.Format("{0} / {1} = {2}", 10, 15, Calculator.Divide(10, 15)));

List<PersonModel> users = DataAccess.GetAllPeople();
users.Dump();

DataAccess.AddNewPerson(new PersonModel { FirstName = "Danny", LastName = "Verdel" });
users = DataAccess.GetAllPeople();
users.Dump();

Console.ReadLine();