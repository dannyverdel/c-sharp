using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.StrategyPattern
{
    /*
     * The Strategy Design Pattern is a behavioral design pattern that allows you to define a family of interchangeable algorithms and make them easily switchable at runtime. 
     * The pattern is useful when you have multiple algorithms that can be used to achieve a specific task, 
     * and you want to be able to switch between them without changing the code that uses them.
     * 
     * The strategy pattern involves defining a set of related algorithms, encapsulating each one, and making them interchangeable. 
     * Each algorithm is defined in a separate class that implements a common interface. The client code can then choose which algorithm to use at runtime, 
     * without knowing the details of how the algorithm works.
     * 
     * In this example, we have defined three concrete strategies for performing mathematical operations: addition, subtraction, and multiplication. 
     * Each strategy implements the IStrategy interface, which defines a DoOperation method that takes two integers as input and returns an integer.
     * 
     * We have also defined a Context class that takes an IStrategy object in its constructor 
     * and has a ExecuteStrategy method that uses the selected strategy to perform a mathematical operation on two integers.
     * 
     * Finally, we create an instance of the Context class with each of the three strategies and use the ExecuteStrategy method to perform a mathematical operation. 
     * The output of each operation is stored in the result variable.
     */
    public class InvokeStrategyPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            Context context = new Context(new OperationAdd());
            int result = context.ExecuteStrategy(10, 5); // result = 15
            result.Dump();

            context = new Context(new OperationSubtract());
            result = context.ExecuteStrategy(10, 5); // result = 5
            result.Dump();

            context = new Context(new OperationMultiply());
            result = context.ExecuteStrategy(10, 5); // result = 50
            result.Dump();
        }
    }

    public interface IStrategy
    {
        int DoOperation(int n1, int n2);
    }

    // Define the concrete strategies
    public class OperationAdd : IStrategy
    {
        public int DoOperation(int n1, int n2) =>  n1 + n2;
    }

    public class OperationSubtract : IStrategy
    {
        public int DoOperation(int n1, int n2) => n1 - n2;
    }

    public class OperationMultiply : IStrategy
    {
        public int DoOperation(int n1, int n2) => n1 * n2;
    }

    public class Context
    {
        private IStrategy _strategy;
        public Context(IStrategy strategy) => _strategy = strategy;
        public int ExecuteStrategy(int n1, int n2) => _strategy.DoOperation(n1, n2);
    }
}

