namespace DemoLibrary;

public class ProductModel
{
    public string ItemName { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public override string ToString() => $"{ItemName}: ${Price}";
}