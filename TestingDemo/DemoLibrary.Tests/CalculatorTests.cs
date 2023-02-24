namespace DemoLibrary.Tests;

public class CalculatorTests
{
    [Theory]
    [InlineData(21, 5.25, 26.25)]
    [InlineData(double.MaxValue, 5, double.MaxValue)]
    public void AddSimpleValuesShouldCalculate(double x, double y, double expected) {
        // Arrange

        // Act
        double actual = Calculator.Add(x, y);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(8, 4, 4)]
    [InlineData(double.MaxValue, double.MaxValue, 0)]
    public void SubtractSimpleValuesShouldCalculate(double x, double y, double expected) {
        double actual = Calculator.Subtract(x, y);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(8, 4, 2)]
    [InlineData(10, 2, 5)]
    public void DivideSimpleValuesShouldCalculate(double x, double y, double expected) {
        double actual = Calculator.Divide(x, y);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DivideByZero() {
        double expected = 0;
        double actual = Calculator.Divide(10, 0);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(10, 5, 50)]
    [InlineData(10.5, 10, 105)]
    public void MultiplySimpleValuesShouldCalculate(double x, double y, double expected) {
        double actual = Calculator.Multiply(x, y);
        Assert.Equal(expected, actual);
    }
}

