namespace ShopAndStore.Logic.Products;

public abstract class Product
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public int MinInShop { get; protected set; }

    protected decimal PurchasePrice { get; set; } = 1000;
    protected decimal BaseSalePrice { get; set; } = 2000;

    public decimal GetSalePrice() => BaseSalePrice;
    public decimal GetProfit(EnvironmentInformation environment) => GetPriceWithDiscount(environment) - PurchasePrice;
    public abstract decimal GetPriceWithDiscount(EnvironmentInformation environment);
    public abstract DateOnly GetArrivalDate(EnvironmentInformation environment);
}
