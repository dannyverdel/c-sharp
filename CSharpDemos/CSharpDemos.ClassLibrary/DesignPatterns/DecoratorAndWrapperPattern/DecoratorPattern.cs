using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.DecoratorAndWrapperPattern
{
    /*
     * The Decorator pattern is a design pattern that allows you to add behavior to an existing object, 
     * dynamically, without affecting the behavior of other objects from the same class. 
     * The Decorator pattern is a structural pattern that involves a set of decorator classes that are used to wrap concrete components.
     * 
     * A decorator class wraps the original component class and adds additional behavior. 
     * The decorator class has a has-a relationship with the component class, meaning that it contains an instance of the component class as a member.
     * 
     * In this example, we have a IBeverage interface that defines the behavior of a beverage. 
     * The Espresso class implements the IBeverage interface and provides the concrete implementation of the GetDescription and GetCost methods. 
     * The Mocha class acts as a decorator and adds additional behavior to the Espresso object. 
     * The Mocha class wraps the Espresso object and adds the description "Mocha" to the existing description of the Espresso object. 
     * It also adds an additional cost of 0.20 to the existing cost of the Espresso object.
     */

    public class InvokeDecoratorPattern : IInvokeMethod
	{
		public void InvokeMethod()
		{
            IBeverage beverage = new Espresso();
            beverage = new Mocha(beverage);
            beverage = new Mocha(beverage);

            Console.WriteLine(beverage.GetDescription() + " $" + beverage.GetCost());
        }
    }

    public interface IBeverage
    {
        string GetDescription();
        double GetCost();
    }

    public class Espresso : IBeverage
    {
        public string GetDescription() => "Espresso";
        public double GetCost() => 1.99;
    }

    public class Mocha : IBeverage
    {
        private IBeverage _beverage;
        public Mocha(IBeverage beverage) => _beverage = beverage;
        public string GetDescription() => _beverage.GetDescription() + ", Mocha";
        public double GetCost() => 0.20 + _beverage.GetCost();
    }

}