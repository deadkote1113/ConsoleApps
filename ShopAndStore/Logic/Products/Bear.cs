namespace ShopAndStore.Logic.Products;

public class Bear : Product
{
    public Bear()
    {
        Id = 2;
        Name = "Bear";
        MinInShop = 5;
    }

    public override DateOnly GetArrivalDate(EnvironmentInformation environment)
    {
        if (environment.CurrentDate.IsHolliday())
            return environment.CurrentDate.AddDays(3);
        else
            return environment.CurrentDate.AddDays(2);
    }

    public override decimal GetPriceWithDiscount(EnvironmentInformation environment)
    {
        if (environment.CustomerNumber % 5 == 0)
            return BaseSalePrice * 0.945m;
        else
            return BaseSalePrice;
    }
}
