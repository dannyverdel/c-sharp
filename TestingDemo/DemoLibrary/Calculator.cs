using System;
namespace DemoLibrary;

public static class Calculator
{
    public static double Add(double x, double y) => x + y;
    public static double Subtract(double x, double y) => x - y;
    public static double Multiply(double x, double y) => x * y;
    public static double Divide(double x, double y) => y == 0 ? 0 : x / y;
}

