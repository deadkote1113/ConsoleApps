using ShopAndStore.Logic.Products;

namespace ShopAndStore.Logic;

public class Store
{
    public const string HandlerName = "store";

    private Dictionary<string, Func<string[], EnvironmentInformation, HandleAnswer>> _handlers = new();

    private Dictionary<int, Product> _products = new();

    private Dictionary<ProductIdWithArrivalTime, DeliveryInformation> _producstInDelivery = new();

    public Store()
    {
        _handlers.Add("getProductList", GetProductList);
        _handlers.Add("order", Order);

        var cola = new Cola();
        _products.Add(cola.Id, cola);
        var bear = new Bear();
        _products.Add(bear.Id, bear);
        var hookahTobacco = new HookahTobacco();
        _products.Add(hookahTobacco.Id, hookahTobacco);
        var kFCChickenWings = new KFCChickenWings();
        _products.Add(kFCChickenWings.Id, kFCChickenWings);
    }

    public HandleAnswer Handle(string method, string[] args, EnvironmentInformation environment) => _handlers[method](args, environment);

    public string GetProductOnDelivery(DateOnly date)
    {
        var result = "Id\tName\t\tOrder count\tArrival date\n";
        foreach (var productInDelivery in _producstInDelivery)
        {
            var product = productInDelivery.Value.Product;
            var count = productInDelivery.Value.Count;

            result += $"{product.Id}\t{product.Name}\t{count}\t{productInDelivery.Key.Date}\n";
        }
        return result;
    }

    public void OrderProducts(Dictionary<int, int> productIdToCount, EnvironmentInformation environment)
    {
        foreach (var item in productIdToCount)
        {
            var productId = item.Key;
            var count = item.Value;

            var product = _products[productId];
            var arrivalDay = product.GetArrivalDate(environment);

            var key = new ProductIdWithArrivalTime(productId, arrivalDay);
            if (_producstInDelivery.ContainsKey(key))
                _producstInDelivery[key].Count += count;
            else
                _producstInDelivery.Add(key, new(product, count));
        }
    }

    #region Handlers

    private HandleAnswer GetProductList(string[] args, EnvironmentInformation environment)
    {
        var responseText = "Id\tName\tBase price\tArrival day\n";
        foreach (var item in _products)
        {
            var product = item.Value;
            var arrivalDay = product.GetArrivalDate(environment);
            var salePrice = product.GetSalePrice();

            responseText += $"{product.Id}\t{product.Name}\t{salePrice}\t{arrivalDay}\n";
        }
        return new(true, new(responseText, 0, false), null);
    }

    private HandleAnswer Order(string[] args, EnvironmentInformation environment)
    {
        var productId = int.Parse(args[0]);
        if (!_products.ContainsKey(productId))
            return new(false, null, $"There is no product with such productId {productId}");

        var product = _products[productId];
        var arrivalDay = product.GetArrivalDate(environment);

        var key = new ProductIdWithArrivalTime(productId, arrivalDay);
        if (_producstInDelivery.ContainsKey(key))
            _producstInDelivery[key].Count += 1;
        else
            _producstInDelivery.Add(key, new(product, 1));

        return new(true, new($"The product has been successfully ordered and will arrive at the store {arrivalDay}", 0, true), null);
    }
    #endregion
}

internal record ProductIdWithArrivalTime(int ProductId, DateOnly Date) { }


internal class DeliveryInformation
{
    public DeliveryInformation(Product product, int count)
    {
        Product = product;
        Count = count;
    }

    public int Count { get; set; }
    public Product Product { get; set; }
}