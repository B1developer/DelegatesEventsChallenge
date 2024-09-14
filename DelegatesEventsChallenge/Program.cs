using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Default; // For Currency Symbols in Console.WriteLine
        // Create a stock object
        Stock appleStock = new Stock("Apple", 150.00m);

        // Create an alert system
        StockAlert stockAlert = new StockAlert();

        // Subscribe the alert system to the stock's price change event
        appleStock.OnPriceChanged += stockAlert.OnPriceChanged;

        // Simulate price updates
        appleStock.UpdatePrice(155.00m);  // No significant change
        appleStock.UpdatePrice(165.00m);  // Significant change, alert will trigger
        appleStock.UpdatePrice(160.00m);  // No significant change
        appleStock.UpdatePrice(180.00m);  // Significant change, alert will trigger
    }
}

// Step 1: Declare the delegate for the event
public delegate void PriceChangedEventHandler(decimal oldPrice, decimal newPrice);

// Step 2: Stock class that tracks the stock price and raises an event
public class Stock
{
    public string StockName { get; set; }
    private decimal _price;

    // Step 3: Declare an event using the delegate
    public event PriceChangedEventHandler OnPriceChanged;

    public Stock(string stockName, decimal initialPrice)
    {
        StockName = stockName;
        _price = initialPrice;
    }

    // Method to update the price and check for significant change
    public void UpdatePrice(decimal newPrice)
    {
        decimal changePercentage = Math.Abs((newPrice - _price) / _price) * 100;

        // Raise the event if the price change is more than 5%
        if (changePercentage > 5 && OnPriceChanged != null)
        {
            OnPriceChanged(_price, newPrice);
        }

        // Update the current price
        _price = newPrice;
        Console.WriteLine($"{StockName} price updated to {newPrice:C}");
    }
}

// Step 4: StockAlert class that listens for price changes and alerts the user
public class StockAlert
{
    public void OnPriceChanged(decimal oldPrice, decimal newPrice)
    {
        Console.WriteLine($"ALERT: Significant price change detected! Old Price: {oldPrice:C}, New Price: {newPrice:C}");
    }
}