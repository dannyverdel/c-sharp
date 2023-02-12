using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.FacadePattern
{
    /*
     * The Facade design pattern provides a simplified interface to a complex system. 
     * It acts as a wrapper around the complex system and presents a more user-friendly interface to the client. 
     * This way, the client doesn't need to interact directly with the complex system and can instead use the simpler interface provided by the Facade.
     * 
     * In this example, the ComplexSystem class represents the complex system that we want to wrap with a simpler interface. 
     * The Facade class acts as the wrapper around the complex system and provides a simplified interface to the client through its methods MethodA and MethodB. 
     * The client can then interact with the complex system by calling these methods on the Facade class instead of interacting directly with the ComplexSystem.
     */

    public class InvokeFacadePattern : IInvokeMethod
	{
		public void InvokeMethod()
        {
            ComplexSystem complex_system = new ComplexSystem();
            Facade facade = new Facade(complex_system);

            facade.MethodA();
            facade.MethodB();
        }
    }

    public class ComplexSystem
    {
        public void MethodA() => Console.WriteLine("Complex System - Method A");
        public void MethodB() => Console.WriteLine("Complex System - Method B");
    }

    public class Facade
    {
        private ComplexSystem _complex_system;

        public Facade(ComplexSystem complexSystem) => _complex_system = complexSystem;

        public void MethodA()
        {
            Console.WriteLine("Facade - Method A");
            _complex_system.MethodA();
        }

        public void MethodB()
        {
            Console.WriteLine("Facade - Method B");
            _complex_system.MethodB();
        }
    }
}

