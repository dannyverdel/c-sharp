using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.AdapterPattern
{

    /*
     * If you have a third-party class Rectangle with the method CalculateArea, 
     * but your client requires an interface IShape with the method Area. 
     * In this scenario, you could create an adapter class RectangleAdapter that implements the IShape interface and delegates calls to the Rectangle class.
     * 
     * In this example, the RectangleAdapter acts as a bridge between the Rectangle class and the IShape interface, 
     * allowing the client to use the Rectangle class as if it implements the IShape interface.
     * 
     * To use the RectangleAdapter with a client that needs a Rectangle object, 
     * you would create an instance of the Rectangle class, and pass it to the constructor of the RectangleAdapter. 
     * Then, you can use the adapter as if it were a Rectangle object, because it implements the IShape interface, which the client is expecting.
     */

    public class InvokeAdapterPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            Rectangle rectangle = new Rectangle() { Width = 10, Height = 20 };
            IShape shape = new RectangleAdapter(rectangle);

            Console.WriteLine("Area of the rectangle: " + shape.Area());
        }
    }

    public interface IShape
    {
        double Area();
    }

    public class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public double CalculateArea()
        {
            return Width * Height;
        }
    }

    public class RectangleAdapter : IShape
    {
        private Rectangle _rectangle;

        public RectangleAdapter(Rectangle rectangle)
        {
            _rectangle = rectangle;
        }

        public double Area()
        {
            return _rectangle.CalculateArea();
        }
    }
}

