namespace DemoLibrary;

public class ShoppingCartModel
{
    public delegate void MentionDiscount(decimal sub_total);

    public List<ProductModel> Items { get; set; } = new List<ProductModel>();

    public decimal GenerateTotal(MentionDiscount mention_subtotal, Func<List<ProductModel>, decimal, decimal> calculate_discounted_total, Action<string> tell_user_we_are_discounting) {
        decimal sub_total = Items.Sum(x => x.Price);
        mention_subtotal(sub_total);
        tell_user_we_are_discounting("We are applying your discount.");

        return calculate_discounted_total(Items, sub_total);
    }
}