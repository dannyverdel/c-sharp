using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.CompositePattern
{
    /*
     * The composite design pattern is a structural design pattern that allows you to build complex structures from simple building blocks. 
     * It lets you treat an individual object and a group of objects in the same way.
     * 
     * n C#, a common example of the composite pattern is creating a tree-like structure of objects to represent part-whole hierarchies. 
     * For example, you might have a "Graphic" class that represents a basic graphical object, and a "CompositeGraphic" class that represents a group of graphics. 
     * The CompositeGraphic class would have methods for adding and removing graphics, as well as a method for drawing the entire group of graphics.
     * 
     * In this example, we create a CompositeGraphic object called root and add two SimpleGraphic objects to it. 
     * Then we create another CompositeGraphic object called subgroup and add two more SimpleGraphic objects to it. 
     * Finally, we add the subgroup to the root object. When we call the Draw method on the root object, 
     * it will recursively draw all the graphics in the hierarchy, including the SimpleGraphic objects and the subgroup.
     */

    public class InvokeCompositePattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            CompositeGraphic root = new CompositeGraphic();
            root.Add(new SimpleGraphic());
            root.Add(new SimpleGraphic());

            CompositeGraphic subgroup = new CompositeGraphic();
            subgroup.Add(new SimpleGraphic());
            subgroup.Add(new SimpleGraphic());

            root.Add(subgroup);

            root.Draw();
        }
    }

    abstract class Graphic
    {
        public abstract void Draw();
    }

    class SimpleGraphic : Graphic
    {
        public override void Draw() => Console.WriteLine("Drawing a simple graphic.");
    }

    class CompositeGraphic : Graphic
    {
        private List<Graphic> _graphics = new List<Graphic>();

        public override void Draw()
        {
            Console.WriteLine("Drawing a composite graphic.");

            foreach (Graphic graphic in _graphics)
                graphic.Draw();
        }

        public void Add(Graphic graphic) => _graphics.Add(graphic);

        public void Remove(Graphic graphic) => _graphics.Remove(graphic);
    }

}

