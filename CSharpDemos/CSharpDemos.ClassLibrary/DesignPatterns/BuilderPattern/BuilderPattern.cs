using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.BuilderPattern
{
    /*
     * The Builder Design Pattern is a creational pattern that allows you to build objects 
     * incrementally by separating the construction of an object from its representation. 
     * It provides a flexible way to create complex objects step by step, while still ensuring that the end result is a valid object.
     * 
     * You can implement the Builder Design Pattern by defining a "Builder" class that has methods for setting each property of the object being constructed. 
     * The "Director" class then uses the Builder class to build the object step by step, until it's ready to be returned.
     * 
     * With this implementation, you can create a complex object like a car, step by step, 
     * and have full control over each property that gets set, while ensuring that the end result is a valid object.
     */

    public class InvokeBuilderPattern : IInvokeMethod
	{
		public void InvokeMethod()
        {
            CarBuilder builder = new CarBuilder();
            CarDirector director = new CarDirector(builder);
            Car sports_car = director.ConstructSportsCar();
            sports_car.Dump();
        }
    }

    public class Car
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Engine { get; set; }
        public string? Transmission { get; set; }
        public string? PaintColor { get; set; }
    }

    public class CarBuilder
    {
        private Car _car = new Car();

        public CarBuilder SetMake(string make)
        {
            _car.Make = make;
            return this;
        }

        public CarBuilder SetModel(string model)
        {
            _car.Model = model;
            return this;
        }

        public CarBuilder SetYear(int year)
        {
            _car.Year = year;
            return this;
        }

        public CarBuilder SetEngine(string engine)
        {
            _car.Engine = engine;
            return this;
        }

        public CarBuilder SetTransmission(string transmission)
        {
            _car.Transmission = transmission;
            return this;
        }

        public CarBuilder SetPaintColor(string color)
        {
            _car.PaintColor = color;
            return this;
        }

        public Car Build()
        {
            return _car;
        }
    }

    public class CarDirector
    {
        private CarBuilder _car_builder;

        public CarDirector(CarBuilder builder)
        {
            _car_builder = builder;
        }

        public Car ConstructSportsCar()
        {
            return _car_builder
                .SetMake("Lamborghini")
                .SetModel("Aventador")
                .SetYear(2022)
                .SetEngine("V12")
                .SetTransmission("Automatic")
                .SetPaintColor("Red")
                .Build();
        }
    }
}

