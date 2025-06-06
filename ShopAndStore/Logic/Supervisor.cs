using ShopAndStore.Logic.InputOutput;

namespace ShopAndStore.Logic;

public class Supervisor
{
    private readonly Random _random = new();
    private readonly Dictionary<string, Func<string, string[], EnvironmentInformation, HandleAnswer>> _handlers = new();

    private readonly InputHandler _inputHandler = new();
    private readonly ConsoleOutput _consoleOutput = new();
    private readonly Store _store = new();
    private readonly Shop _shop = new();
    private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Now);

    public Supervisor()
    {
        _handlers.Add("shop", _shop.Handle);
        _handlers.Add("store", _store.Handle);
    }

    public void StartWork()
    {
        while (true)
        {
            var profit = 0m;
            var customersCount = GetTodayCustomersCount();
            _consoleOutput.Write(GetWelcomeMessage(customersCount));
            for (int i = 0; i < customersCount; i++)
            {
                profit += HandleCustomer(i + 1);
            }
            var endingProducts = _shop.GetEndingProducts();
            _store.OrderProducts(endingProducts, new(_currentDate, -1));
            _consoleOutput.Write(GetEndDayMessage(profit));
            _currentDate.AddDays(1);
        }
    }

    private decimal HandleCustomer(int customerNumber)
    {
        var environment = new EnvironmentInformation(_currentDate, customerNumber);
        var isDayEnds = false;
        decimal profit = 0;
        do
        {
            var handleIsSuccess = false;
            HandlerResult? handleResult;
            do
            {
                var inputResult = GetInput();

                var handleAnswer = _handlers[inputResult!.WhoWillHandle](inputResult.HandleMethod, inputResult.Args, environment);

                handleIsSuccess = handleAnswer.IsSuccess;
                if (!handleIsSuccess)
                    _consoleOutput.Write("", handleAnswer.Error!);
                handleResult = handleAnswer.Response;
            }
            while (!handleIsSuccess);

            _consoleOutput.Write(handleResult!.TextResponse);
            profit += handleResult!.Profit;
            isDayEnds = handleResult.IsDayEnds;
        }
        while (!isDayEnds);
        return profit;
    }

    private ParseResult GetInput()
    {
        bool inputIsCorrect = false;
        ParseResult? inputResult;
        do
        {
            var inputAnswer = _inputHandler.Handle();
            inputIsCorrect = inputAnswer.IsSuccess;
            if (!inputIsCorrect)
                _consoleOutput.Write("", inputAnswer.error!);
            inputResult = inputAnswer.result;
        } while (!inputIsCorrect);

        return inputResult!;
    }

    private int GetTodayCustomersCount() => _random.Next(5, 10);

    private string GetWelcomeMessage(int customersCount) => $"Today {_currentDate} the store has opened and will serve {customersCount} visitors";

    private string GetEndDayMessage(decimal profit) => $"The store's profit was {profit}\r\nGoods were ordered from the store\r\n{_store.GetProductOnDelivery(_currentDate)}";

}
