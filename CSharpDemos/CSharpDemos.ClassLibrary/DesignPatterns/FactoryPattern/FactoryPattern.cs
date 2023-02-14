using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.FactoryPattern
{
    /*
     * The Factory Design Pattern is a creational design pattern that provides an interface for creating objects in a superclass, 
     * but allows subclasses to alter the type of objects that will be created.
     * 
     * In other words, it's a way of creating objects without specifying the exact class of object that will be created. 
     * This is useful when a class can't anticipate the type of objects it needs to create. Instead of creating an object directly, 
     * it asks the factory object to do that for it.
     * 
     * In this example, the AnimalFactory class acts as a factory for creating IAnimal objects. 
     * The GetAnimal method returns an instance of either Dog or Cat based on the string argument passed in. 
     * The InvokeFactoryPattern class uses the factory to create instances of IAnimal objects, without having to know the exact class of the object that will be created.
     */

    public class InvokeFactoryPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            IAnimal animal = AnimalFactory.GetAnimal("dog");
            animal.Speak();

            animal = AnimalFactory.GetAnimal("cat");
            animal.Speak();
        }
    }

    public interface IAnimal
    {
        void Speak();
    }

    public class Dog : IAnimal
    {
        public void Speak()
        {
            Console.WriteLine("Woof!");
        }
    }

    public class Cat : IAnimal
    {
        public void Speak()
        {
            Console.WriteLine("Meow!");
        }
    }

    public class AnimalFactory
    {
        public static IAnimal GetAnimal(string animal_name)
        {
            switch (animal_name)
            {
                case "dog":
                    return new Dog();
                case "cat":
                    return new Cat();
                default:
                    throw new ArgumentException("Invalid animal name");
            }
        }
    }
}

