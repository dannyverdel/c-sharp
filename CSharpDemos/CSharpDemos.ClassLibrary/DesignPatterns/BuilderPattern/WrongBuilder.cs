using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.BuilderPattern.Builder.WrongBuilder
{
    public class InvokeWrongBuilder : IInvokeMethod
    {
        public void InvokeMethod()
        {
            CarBuilder builder = new CarBuilder();

            BuildRedLada1980(builder);

            builder.Build().Dump();
        }

        public void BuildRedLada1980(ICarBuilder builder)
        {
            builder.SetMake("lada")
                .SetColour("red")
                .SetManifactureDate("01/01/1980");
        }
    }

    public class Car
    {
        public string? Make { get; set; }
        public string? Colour { get; set; }
        public string? ManifactureDate { get; set; }
    }

    public interface ICarBuilder
    {
        public ICarBuilder SetMake(string make);

        public ICarBuilder SetColour(string colour);

        public ICarBuilder SetManifactureDate(string date);
    }

    public class CarBuilder  : ICarBuilder
    {
        private Car _car = new Car();

        public ICarBuilder SetMake(string make)
        {
            _car.Make = make;
            return this;
        }

        public ICarBuilder SetColour(string colour)
        {
            _car.Colour = colour;
            return this;
        }

        public ICarBuilder SetManifactureDate(string date)
        {
            _car.ManifactureDate = date;
            return this;
        }

        public Car Build() => _car;
    }
}

