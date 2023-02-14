using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.ChainOfResponsibilityPattern
{
    /*
     * The Chain of Responsibility design pattern is a design pattern that is used to create a chain of objects. 
     * Each object in the chain has the opportunity to handle a request or pass it on to the next object in the chain. 
     * This allows multiple objects to be linked together to form a pipeline of processing, making it easier to add, remove, or modify processing steps in the pipeline.
     * 
     * In this example, ConcreteHandlerA, ConcreteHandlerB, and ConcreteHandlerC are the concrete handlers in the chain, 
     * and the Handler class is the abstract handler that defines the common interface for the concrete handlers. 
     * The HandleRequest method of each concrete handler checks if it can handle the request, and if it can't, 
     * it passes the request on to the next handler in the chain by calling the HandleRequest method of the NextHandler.
     * 
     * This shows that each request is handled by the appropriate concrete handler in the chain, 
     * starting with ConcreteHandlerA and moving down the chain to ConcreteHandlerB and then to ConcreteHandlerC. 
     * If a request cannot be handled by a particular concrete handler, it is passed on to the next handler in the chain.
     */

    public class InvokeChainOfResponsibilityPatttern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            Handler a = new ConcreteHandlerA();
            Handler b = new ConcreteHandlerB();
            Handler c = new ConcreteHandlerC();

            a.SetNext(b);
            b.SetNext(c);

            a.HandleRequest(5);
            a.HandleRequest(15);
            a.HandleRequest(25);
        }
    }

	abstract class Handler
	{
		protected Handler? NextHandler;
		public void SetNext(Handler next) => NextHandler = next;
		public abstract void HandleRequest(int request);
	}

    class ConcreteHandlerA : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 0 && request < 10)
                Console.WriteLine("ConcreteHandlerA handled the request.");
            else if (NextHandler != null)
                NextHandler.HandleRequest(request);
        }
    }

    class ConcreteHandlerB : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 10 && request < 20)
                Console.WriteLine("ConcreteHandlerB handled the request.");
            else if (NextHandler != null)
                NextHandler.HandleRequest(request);
        }
    }

    class ConcreteHandlerC : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 20 && request < 30)
                Console.WriteLine("ConcreteHandlerC handled the request.");
            else if (NextHandler != null)
                NextHandler.HandleRequest(request);
        }
    }
}

