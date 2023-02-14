using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.InterpreterPattern
{
    /*
	 * Interpreter design pattern is a behavioral design pattern that allows defining a grammar for a language and then using it to interpret sentences in that language. 
	 * The pattern provides a way to parse the language sentences and execute actions according to the grammar rules.
	 * 
	 * In this example, we have an abstract class AbstractExpression that represents the grammar rule. 
	 * The two concrete classes TerminalExpression and NonterminalExpression represent the terminal and non-terminal symbols in the grammar. 
	 * The Context class holds the input string, and the output string is stored in it. In the Main method, we create the expressions, interpret them and finally output the result.
	 */

	public class InvokeInterpreterPattern : IInvokeMethod
	{
		public void InvokeMethod()
		{
			Context context = new Context("I am a software engineer");

			List<AbstractExpression> expressions_list = new List<AbstractExpression>
			{
			new TerminalExpression("I"),
			new TerminalExpression("am"),
			new TerminalExpression("a"),
			new TerminalExpression("software"),
			new TerminalExpression("engineer")
			};

			for (int i = 0; i < expressions_list.Count - 1; i++)
			expressions_list[i] = new NonTerminalExpression(expressions_list[i], expressions_list[i + 1]);

			expressions_list[expressions_list.Count - 1].Interpret(context);

			context.Output.Dump();
		}
	}

	public abstract class AbstractExpression
	{
		public abstract void Interpret(Context context);
	}

	public class TerminalExpression : AbstractExpression
	{
		private string _data;
		public TerminalExpression(string data) => _data = data;
		public override void Interpret(Context context) => context.Output = _data + " ";
	}

	public class NonTerminalExpression : AbstractExpression
	{
		private AbstractExpression _expression1;
		private AbstractExpression _expression2;

		public NonTerminalExpression(AbstractExpression expression1, AbstractExpression expression2)
		{
			_expression1 = expression1;
			_expression2 = expression2;
		}

        public override void Interpret(Context context)
        {
			_expression1.Interpret(context);
			_expression2.Interpret(context);
        }
    }

	public class Context
	{
		public string Input { get; set; }
		public string Output { get; set; }
		public Context(string input)
		{
			Input = input;
			Output = string.Empty;
		}
	}
}

