using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.MementoPattern
{
    /*
     * The Memento design pattern is a behavioral design pattern that provides a way to capture and restore the state of an object without violating encapsulation. 
     * In other words, it allows you to save the current state of an object, and later restore that state to the object.
     * 
     * In this example, the Editor class is the originator, the EditorMemento class is the memento, and the History class is the caretaker. 
     * The Editor class has a Save method that creates a new EditorMemento object to store the current state of the editor. 
     * The Editor class also has a Restore method that restores the state of the editor from an EditorMemento object. 
     * The History class is responsible for storing the EditorMemento objects and providing a way to retrieve them.
     * 
     * The example creates an Editor object and sets its initial text. The current state of the editor is saved using the Push method of the History object. 
     * The text is then changed, and the previous state is restored using the Pop method of the History object. 
     * Finally, the current text of the editor is printed to the console, which should be "Hello, world!" after the state is restored.
     */

    public class InvokeMementoPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            Editor editor = new Editor();
            History history = new History();

            // Set initial text
            editor.Text = "Hello, world!";

            // Save the current state of the editor
            history.Push(editor.Save());

            // Change the text
            editor.Text = "Goodbye, world!";

            // Restore the previous state of the editor
            editor.Restore(history.Pop());

            // The editor text should now be "Hello, world!"
            Console.WriteLine(editor.Text);
        }
    }

    public class Editor
    {
        public string? Text { get; set; }

        public EditorMemento Save() => new EditorMemento(Text ?? "");
        public void Restore(EditorMemento memento) => Text = memento.Text;
    }

    public class EditorMemento
    {
        private readonly string _text;
        public EditorMemento(string text) => _text = text;
        public string Text => _text;
    }

    public class History
    {
        private List<EditorMemento> _mementos = new List<EditorMemento>();
        public void Push(EditorMemento memento) => _mementos.Add(memento);
        public EditorMemento Pop()
        {
            EditorMemento memento = _mementos.Last();
            _mementos.Remove(memento);
            return memento;
        }
    }
}

