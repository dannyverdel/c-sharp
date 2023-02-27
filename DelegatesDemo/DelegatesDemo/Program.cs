
using DemoLibrary;

namespace DelegatesDemo
{
    class Program
    {
        public static ShoppingCartModel cart = new ShoppingCartModel();
        static void Main(string[] args) {
            PopulateCartWithDemoData();

            Console.WriteLine($"The total for the cart is {cart.GenerateTotal(SubTotalAlert, CalculateLeveledDiscount, AlertUser):C2}");

            decimal total = cart.GenerateTotal(sub_total => Console.WriteLine($"The subtotal for cart 2 is {sub_total:C2}"), (products, sub_total) => {
                if ( sub_total > 100 ) return sub_total * 0.80M;
                else if ( sub_total > 50 ) return sub_total * 0.85M;
                else if ( sub_total > 10 ) return sub_total * 0.95M;
                else return sub_total;
            }, message => Console.WriteLine(message));

            //CustomListDemo();

            Console.ReadKey();
        }

        private static void SubTotalAlert(decimal sub_total) => Console.WriteLine($"The subtotal is {sub_total:C2}");

        private static void AlertUser(string message) => Console.WriteLine(message);

        private static decimal CalculateLeveledDiscount(List<ProductModel> items, decimal sub_total) {
            if ( sub_total > 100 ) return sub_total * 0.80M;
            else if ( sub_total > 50 ) return sub_total * 0.85M;
            else if ( sub_total > 10 ) return sub_total * 0.95M;
            else return sub_total;
        }

        private static void PopulateCartWithDemoData() {
            cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 3.63M });
            cart.Items.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
            cart.Items.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
            cart.Items.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });
        }

        // CustomList class has an example of Func delegates
        private static void CustomListDemo() {
            CustomList<ProductModel> products = new CustomList<ProductModel>();
            products.Add(new ProductModel { ItemName = "Cereal", Price = 3.63M });
            products.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
            products.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
            products.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });


            ProductModel product = products.Where(x => x.ItemName == "Strawberries").First();
            products.Remove(product);

            Console.WriteLine(products.ToString(true));
        }
    }
}