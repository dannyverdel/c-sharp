using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.TemplateMethodPattern
{
    /*
     * The template method design pattern is a behavioral design pattern that allows you to define the skeleton of an algorithm in a base class, 
     * while letting its subclasses implement specific steps of the algorithm.
     * 
     * In other words, the template method defines the overall steps of a process, 
     * while leaving the implementation details to be filled in by specific subclasses. 
     * This approach helps to ensure that the process is consistent across all subclasses, 
     * while allowing for customization and variation in the specific implementation of each subclass.
     * 
     * In this example, CoffeeMaker is an abstract base class that defines the overall process for making coffee. 
     * Subclasses BlackCoffeeMaker and LatteMaker then provide specific implementations of the BrewCoffee() and AddCondiments() methods.
     * 
     * When you call MakeCoffee() on a CoffeeMaker object, it will execute the overall process, 
     * but the specific steps of brewing and adding condiments will be customized based on the subclass that you're using.
     */
    public class InvokeTemplateMethodPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            CoffeeMaker coffee = new BlackCoffeeMaker();
            coffee.MakeCoffee();

            CoffeeMaker latte = new LatteMaker();
            latte.MakeCoffee();
        }
    }

    public abstract class CoffeeMaker
    {
        public void MakeCoffee()
        {
            BoilWater();
            PourInCup();
            BrewCoffee();
            AddCondiments();
        }

        private void BoilWater() => "Boiling water".Dump();
        private void PourInCup() => "Pouring into cup".Dump();
        protected abstract void BrewCoffee();
        protected abstract void AddCondiments();
    }

    public class BlackCoffeeMaker : CoffeeMaker
    {
        protected override void BrewCoffee() => "Brewing black coffee".Dump();
        protected override void AddCondiments() => "No condiments to be added".Dump();
    }

    public class LatteMaker : CoffeeMaker
    {
        protected override void BrewCoffee() => "Brewing espresso".Dump();
        protected override void AddCondiments() => "Adding milk and sugar".Dump();
    }
}

