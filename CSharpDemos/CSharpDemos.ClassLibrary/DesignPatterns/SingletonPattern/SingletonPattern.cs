using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.SingletonPattern
{
    /*
     * Singleton is a creational design pattern that allows only one instance of a class to exist in the entire application. 
     * This is useful when you need to ensure that there is only one object to control the action 
     * throughout the execution and provide a single point of access to it for all other objects.
     * 
     * In this example, the Singleton class has a private constructor, 
     * which ensures that the class can't be instantiated from outside the class. 
     * The Instance property provides the only way to access the singleton object, 
     * and it uses a lock statement to ensure that only one instance of the class is created even in a multi-threaded environment.
     * 
     * This code creates two instances of the Singleton class and then compares them to see if they reference the same object. 
     * Since the singleton pattern only allows for one instance of the class to exist in the entire application, 
     * you should expect the output to be "Both instances reference the same object."
     */

    public class InvokeSingletonPattern : IInvokeMethod
	{
        public void InvokeMethod()
        {
            Singleton instance1 = Singleton.GetInstance();
            Singleton instance2 = Singleton.GetInstance();

            if (instance1 == instance2)
                Console.WriteLine("Both instances reference the same object.");
            else
                Console.WriteLine("The instances reference different objects.");

        }
    }

    public class Singleton
    {
        private static Singleton? _instance;
        private static readonly object _lock_object = new object();

        private Singleton() { }

        public static Singleton GetInstance()
        {
            if (_instance == null)
                lock (_lock_object)
                    if (_instance == null)
                        _instance = new Singleton();

            return _instance;
        }
    }
}

