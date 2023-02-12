using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.FlyweightPattern
{
	public class InvokeFlyweightPattern : IInvokeMethod
	{
        /*
         * Flyweight is a design pattern that is used to reduce the memory usage of an application by sharing objects. 
         * The idea is to store only the data that is unique to each object in memory and reuse objects that have the same data, 
         * rather than having separate objects for each instance.
         * 
         * You can implement the Flyweight design pattern by creating a class for the objects you want to share, 
         * and then using a factory class to manage the creation and sharing of these objects.
         * 
         * In this example, the Character class represents a character that can be displayed on a screen, 
         * and the CharacterFactory class is responsible for managing the creation and sharing of Character objects. 
         * When the GetCharacter method is called, it checks if the requested character already exists, and if not, 
         * it creates a new Character object and stores it in a dictionary. When the same character is requested again, the previously created object is returned, reducing memory usage.
         */

        public void InvokeMethod()
		{
            CharacterFactory factory = new CharacterFactory();
            Character a = factory.GetCharacter('A');
            Character b = factory.GetCharacter('B');
            Character a2 = factory.GetCharacter('A');

            a.Display(10); // Output: A (point size 50)
            b.Display(20); // Output: B (point size 60)

            (a == a2).Dump(); // returns true
        }
    }

	public class Character
	{
		private char _symbol;
		private int _width;
		private int _height;
		private int _ascent;
		private int _descent;
		private int _point_size;

        public Character(char symbol, int width, int height, int ascent, int descent, int pointSize)
        {
            this._symbol = symbol;
            this._width = width;
            this._height = height;
            this._ascent = ascent;
            this._descent = descent;
            this._point_size = pointSize;
        }

        public char Symbol { get { return _symbol; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public int Ascent { get { return _ascent; } }
        public int Descent { get { return _descent; } }
        public int PointSize { get { return _point_size; } }

        public void Display(int point_size)
        {
            Console.WriteLine("{0} (point size {1})", Symbol, this._point_size);
        }
    }

    public class CharacterFactory
    {
        private Dictionary<char, Character> _characters = new Dictionary<char, Character>();

        public Character GetCharacter(char key)
        {
            if (!_characters.ContainsKey(key))
            {
                switch (key)
                {
                    case 'A':
                        _characters[key] = new Character(key, 10, 20, 30, 40, 50);
                        break;
                    case 'B':
                        _characters[key] = new Character(key, 20, 30, 40, 50, 60);
                        break;
                        // ...
                }
            }

            return _characters[key];
        }
    }
}

