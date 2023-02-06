using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.FactoryPattern.Factory
{
    public class InvokeFactoryPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            new NavigationBar();
            new DrowpdownMenu();
        }
    }

    public class NavigationBar
    {
        public NavigationBar() => ButtonFactory.CreateButton();
    }

    public class DrowpdownMenu
    {
        public DrowpdownMenu() => ButtonFactory.CreateButton();
    }

    public class ButtonFactory
    {
        // If you want to change the default button you only have to do it here and not in all the other places you used new Button { Type = "Default Button".Dump() };
        public static Button? CreateButton() { return new Button { Type = "Default Button".Dump() }; }
    }

    public class Button
    {
        public string? Type { get; set; }
    }
}

