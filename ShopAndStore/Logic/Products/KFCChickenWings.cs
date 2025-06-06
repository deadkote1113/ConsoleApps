namespace ShopAndStore.Logic.Products;

public class KFCChickenWings : Product
{
    public KFCChickenWings()
    {
        Id = 4;
        Name = "KFC Chicken Wings";
        MinInShop = 5;
    }

    public override DateOnly GetArrivalDate(EnvironmentInformation environment)
    {
        if (environment.CurrentDate.IsOdd())
            return environment.CurrentDate.AddDays(2);
        else
            return environment.CurrentDate.AddDays(1);
    }

    public override decimal GetPriceWithDiscount(EnvironmentInformation environment)
    {
        if (environment.CurrentDate.IsOdd())
            return BaseSalePrice;
        else
            return BaseSalePrice * 0.8m;
    }
}
