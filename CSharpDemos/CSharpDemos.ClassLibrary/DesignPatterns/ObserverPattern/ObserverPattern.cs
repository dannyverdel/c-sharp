using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.ObserverPattern
{
    /*
	 * The observer design pattern is a design pattern that allows an object, called the subject, to notify other objects, called observers, 
	 * when it undergoes a change in state. In other words, the observers "observe" the subject and are notified automatically when the subject changes. 
	 * This pattern promotes loose coupling between the subject and its observers, making it easy to add or remove observers without affecting the subject.
	 * 
	 * An example of the observer pattern in C# can be a stock market. A stock market broadcasts updates to all of its investors when the value of a particular stock changes. 
	 * The investors (observers) can then choose to react to the change by buying, selling, or ignoring the stock.
	 * 
	 * In this example, the StockMarket class is the subject, and the Investor class is the observer. 
	 * The StockMarket class maintains a list of investors who are interested in the stock's price changes. When the price changes, 
	 * the StockMarket class calls the Update method on each investor to notify them of the change. Investors can choose to buy, sell, or ignore the stock based on the new price.
	 */

    public class InvokeObserverPattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            StockMarket stockMarket = new StockMarket("Microsoft", 100);
            Investor investor1 = new Investor("John");
            Investor investor2 = new Investor("Jane");

            stockMarket.Attach(investor1);
            stockMarket.Attach(investor2);
            stockMarket.UpdateStockPrice(105);  // Output: Notified John of Microsoft's price change to $105.00
                                                //         Notified Jane of Microsoft's price change to $105.00
            stockMarket.Detach(investor1);
            stockMarket.UpdateStockPrice(110);  // Output: Notified Jane of Microsoft's price change to $110.00
        }
    }

    // Define the Subject interface
    public interface ISubject
    {
        void Attach(Investor observer);
        void Detach(Investor observer);
        void Notify();
    }

    // Define the ConcreteSubject class
    public class StockMarket : ISubject
    {
        private string _stockName;
        private decimal _stockPrice;
        private List<Investor> _investors = new List<Investor>();

        public StockMarket(string stockName, decimal stockPrice)
        {
            _stockName = stockName;
            _stockPrice = stockPrice;
        }

        public void Attach(Investor observer) => _investors.Add(observer);
        public void Detach(Investor observer) => _investors.Remove(observer);

        public void Notify()
        {
            foreach (Investor investor in _investors)
                investor.Update(this);
        }

        public void UpdateStockPrice(decimal newPrice)
        {
            _stockPrice = newPrice;
            Notify();
        }

        public decimal GetStockPrice() => _stockPrice;
        public string GetStockName() => _stockName;
    }

    // Define the Observer interface
    public interface IObserver
    {
        void Update(StockMarket stockMarket);
    }

    // Define the ConcreteObserver class
    public class Investor : IObserver
    {
        private string _name;

        public Investor(string name) => _name = name;
        public void Update(StockMarket stockMarket) => Console.WriteLine("Notified {0} of {1}'s price change to {2:C}", _name, stockMarket.GetStockName(), stockMarket.GetStockPrice());
    }
}

