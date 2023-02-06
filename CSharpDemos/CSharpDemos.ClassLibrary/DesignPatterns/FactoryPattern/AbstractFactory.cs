using System;

namespace CSharpDemos.ClassLibrary.DesignPatterns.FactoryPattern.AbstractFactory
{
    public class InvokeAbstractFactoryPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            new NavigationBar(new Apple());
            new DrowpdownMenu(new Apple());

            new NavigationBar(new Android());
            new DrowpdownMenu(new Android());
        }
    }

    public class NavigationBar
    {
        public NavigationBar(IUIFactory factory) => factory.CreateButton();
    }

    // using an interface you can create a button whatever way you want
    public class DrowpdownMenu
    {
        public DrowpdownMenu(IUIFactory factory) => factory.CreateButton();
    }

    public interface IUIFactory
    {
        public Button CreateButton();
    }

    public class Apple : IUIFactory
    {
        public Button CreateButton()
        {
            return new Button { Type = "iOS Button".Dump() };
        }
    }

    public class Android : IUIFactory
    {
        public Button CreateButton()
        {
            return new Button { Type = "Android Button".Dump() };
        }
    }

    public class Button
    {
        public string? Type { get; set; }
    }
}

