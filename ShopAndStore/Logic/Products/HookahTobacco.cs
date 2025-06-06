namespace ShopAndStore.Logic.Products;

public class HookahTobacco : Product
{
    public HookahTobacco()
    {
        Id = 3;
        Name = "Hookah Tobacco";
        MinInShop = 5;
    }

    public override DateOnly GetArrivalDate(EnvironmentInformation environment)
    {
        return environment.CurrentDate.AddDays(1);
    }

    public override decimal GetPriceWithDiscount(EnvironmentInformation environment)
    {
        return BaseSalePrice;
    }
}
