using ShopAndStore.Logic.Products;
using System.ComponentModel;

namespace ShopAndStore.Logic;

public class Shop
{
    public const string HandlerName = "shop";

    private Dictionary<string, Func<string[], EnvironmentInformation, HandleAnswer>> _handlers = new();

    private Dictionary<int, int> _productIdToProductCount = new();
    private Dictionary<int, Product> _productIdToProduct = new();

    public Shop()
    {
        _handlers.Add("getProductList", GetProductList);
        _handlers.Add("buy", Buy);

        var cola = new Cola();
        var bear = new Bear();;
        _productIdToProductCount.Add(cola.Id, 10);
        _productIdToProductCount.Add(bear.Id, 15);

        var hookahTobacco = new HookahTobacco();
        var kFCChickenWings = new KFCChickenWings();
        _productIdToProduct.Add(cola.Id, cola);
        _productIdToProduct.Add(bear.Id, bear);
        _productIdToProduct.Add(hookahTobacco.Id, hookahTobacco);
        _productIdToProduct.Add(kFCChickenWings.Id, kFCChickenWings);
    }

    public HandleAnswer Handle(string method, string[] args, EnvironmentInformation environment) => _handlers[method](args, environment);


    public Dictionary<int, int> GetEndingProducts()
    {
        var result = new Dictionary<int, int>();
        foreach (var item in _productIdToProductCount)
        {
            var productId = item.Key;
            var count = item.Value;

            var minInShop = _productIdToProduct[productId].MinInShop;
            if (count < minInShop)
                result.Add(productId, minInShop);
        }
        return result;
    }

    #region Handlers

    private HandleAnswer GetProductList(string[] args, EnvironmentInformation environment)
    {
        var responseText = "Id\tName\tCount\tPrice\n";
        foreach (var item in _productIdToProductCount)
        {
            var product = _productIdToProduct[item.Key];
            var salePrice = product.GetPriceWithDiscount(environment);
            var count = _productIdToProductCount[item.Key];

            responseText += $"{product.Id}\t{product.Name}\t{count}\t{salePrice}\n";
        }
        return new(true, new(responseText, 0, false), null);
    }

    private HandleAnswer Buy(string[] args, EnvironmentInformation environment)
    {
        var productId = int.Parse(args[0]);
        if (!_productIdToProductCount.ContainsKey(productId))
            return new(false, null, $"There is no product with such productId {productId}");
        if (_productIdToProductCount[productId] == 0)
            return new(false, null, $"Product with id {productId} is run out");

        var profit = _productIdToProduct[productId].GetProfit(environment);
        _productIdToProductCount[productId] -= 1;

        return new(true, new($"Product was successfully purchased", profit, true), null);
    }
    #endregion
}
