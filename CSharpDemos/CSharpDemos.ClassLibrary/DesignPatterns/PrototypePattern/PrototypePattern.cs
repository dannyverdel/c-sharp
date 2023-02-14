using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.PrototypePattern
{
    /*
     * The Prototype design pattern is a creational design pattern that provides a way to create an instance of a 
     * class by copying or cloning an existing object. The existing object serves as a prototype and the newly created object is a copy of the prototype.
     * 
     * In this example, we have an abstract class Prototype that defines the Clone method. 
     * The concrete class ConcretePrototype implements the Clone method and creates a new instance of the object by calling MemberwiseClone method.
     */

    public class InvokePrototypePattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            ConcretePrototype prototype = new ConcretePrototype(10, 20);
            ConcretePrototype clone = (ConcretePrototype)prototype.Clone();

            Console.WriteLine("Original Object: " + prototype.X + ", " + prototype.Y);
            Console.WriteLine("Cloned Object: " + clone.X + ", " + clone.Y);
        }
    }

    abstract class Prototype
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Prototype Clone()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }

    class ConcretePrototype : Prototype
    {
        public ConcretePrototype(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}

