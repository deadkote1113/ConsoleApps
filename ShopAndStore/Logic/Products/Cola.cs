namespace ShopAndStore.Logic.Products;

public class Cola : Product
{
    public Cola()
    {
        Id = 1;
        Name = "Cola";
        MinInShop = 5;
    }

    public override DateOnly GetArrivalDate(EnvironmentInformation environment)
    {
        return environment.CurrentDate.AddDays(2);
    }

    public override decimal GetPriceWithDiscount(EnvironmentInformation environment)
    {
        if (environment.CurrentDate.IsHolliday())
            return BaseSalePrice * 0.9m;
        else
            return BaseSalePrice;
    }
}
