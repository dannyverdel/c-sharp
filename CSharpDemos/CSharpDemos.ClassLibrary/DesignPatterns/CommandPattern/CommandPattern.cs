using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.CommandPattern
{
    /*
	 * The Command design pattern is a behavioral pattern that decouples the sender of a request from the object that executes the request. 
	 * It allows requests to be encapsulated as objects, which can be passed as parameters and stored for later use. 
	 * This pattern provides flexibility, extensibility, and allows for the implementation of undo/redo functionality.
	 * 
	 * In this example, we have a Number class that represents a single integer value, and a Calculator class that can execute ICommand objects, 
	 * store them in a list, and perform undo and redo operations. 
	 * The IncreaseNumberCommand is a concrete implementation of the ICommand interface that increases the value of the Number by a specified amount. 
	 * When the Compute method of the Calculator is called with a command, it executes the command and adds it to the list of commands. 
	 * When the Undo or Redo method is called, it retrieves the previous or next command in the list and either undoes or executes it. 
	 * 
	 * The IncreaseNumberCommand class stores the amount to increase the Number value by, and has an Execute method that increases the value by that amount, 
	 * and an Undo method that decreases the value by that amount. When the Compute method is called with an IncreaseNumberCommand, 
	 * it executes the command by increasing the Number value, and adds the command to the list of executed commands. 
	 * 
	 * The Calculator class keeps track of the list of executed commands, and the current position in the list. When the Undo or Redo method is called, 
	 * it retrieves the previous or next command in the list, and either undoes or executes it. 
	 * The Calculator also ensures that only valid undo or redo operations are performed by checking if there is a previous or next command in the list.
	 */

    public class InvokeCommandPattern : IInvokeMethod
	{
		public void InvokeMethod()
		{
            Number number = new Number(0);
            Calculator calculator = new Calculator();

            ICommand command1 = new IncreaseNumberCommand(number, 5);
            ICommand command2 = new IncreaseNumberCommand(number, 7);

            calculator.Compute(command1); // number.Value is now 5
            number.Dump();

            calculator.Compute(command2); // number.Value is now 12
            number.Dump();

            calculator.Undo(); // number.Value is now 5
            number.Dump();

            calculator.Undo(); // number.Value is now 0
            number.Dump();

            calculator.Redo(); // number.Value is now 5
            number.Dump();

            calculator.Redo(); // number.Value is now 12
            number.Dump();
        }
	}

    // Command interface
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Concrete command
    public class IncreaseNumberCommand : ICommand
    {
        private readonly Number _number;
        private readonly int _value;

        public IncreaseNumberCommand(Number number, int value)
        {
            _number = number;
            _value = value;
        }

        public void Execute() =>_number.Value += _value;
        public void Undo() => _number.Value -= _value;
    }

    // Number class
    public class Number
    {
        public int Value { get; set; }
        public Number(int value) => Value = value;
    }

    // Invoker
    public class Calculator
    {
        private readonly List<ICommand> _commands = new List<ICommand>();
        private int _current = -1;

        public void Compute(ICommand command)
        {
            command.Execute();
            _commands.RemoveRange(_current + 1, _commands.Count - _current - 1);
            _commands.Add(command);
            _current++;
        }

        public void Undo()
        {
            if (_current >= 0)
            {
                _commands[_current].Undo();
                _current--;
            }
        }

        public void Redo()
        {
            if (_current < _commands.Count - 1)
            {
                _current++;
                _commands[_current].Execute();
            }
        }
    }
}