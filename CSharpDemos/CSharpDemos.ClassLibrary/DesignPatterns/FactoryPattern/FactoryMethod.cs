using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.FactoryPattern.FactoryMethod
{
    public class InvokeFactoryMethod : IInvokeMethod
    {
        public void InvokeMethod()
        {
            new NavigationBar();
            new DropdownMenu();

            new AndroidNavigationBar();
            new AndroidDropdownMenu();
        }
    }

    public abstract class Element
    {
        protected abstract Button CreateButton();
        public Element() => CreateButton();
    }

    public class NavigationBar : Element
    {
        protected override Button CreateButton()
        {
            return new Button { Type = "Default Button".Dump() };
        }
    }

    public class DropdownMenu : Element
    {
        protected override Button CreateButton()
        {
            return new Button { Type = "Default Button".Dump() };
        }
    }

    public class AndroidNavigationBar : Element
    {
        protected override Button CreateButton()
        {
            return new Button { Type = "Android Button".Dump() };
        }
    }

    public class AndroidDropdownMenu : Element
    {
        protected override Button CreateButton()
        {
            return new Button { Type = "Android Button".Dump() };
        }
    }

    public class Button
    {
        public string? Type { get; set; }
    }
}

