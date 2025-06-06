namespace ShopAndStore.Logic.InputOutput;

public class InputParser
{
    private List<Func<ParseResult, CheckerResult>> _commandCheckers = new();

    public InputParser()
    {
        _commandCheckers.Add(this.ShopGetProductListChecker);
        _commandCheckers.Add(this.StoreGetProductListChecker);
        _commandCheckers.Add(this.ShopBuyChecker);
        _commandCheckers.Add(this.StoreOrderChecker);
    }

    public InputAnswer Parse(string input)
    {
        var splitedInput = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (splitedInput.Length < 2)
        {
            return new(false, null, "это не команда");
        }
        var result = new ParseResult(splitedInput[0], splitedInput[1], splitedInput[2..]);
        var isCorrect = IsCommandCorrect(result, out var error);
        return isCorrect ? new(isCorrect, result, null) : new(isCorrect, null, error);
    }

    private bool IsCommandCorrect(ParseResult parse, out string? error)
    {
        foreach (var commandChecker in _commandCheckers)
        {
            var result = commandChecker(parse);
            if (result.ArgsProblem)
            {
                error = "Ошибка с аргументами";
                return false;
            }
            if (!result.ArgsProblem && !result.IsNotACommand)
            {
                error = null;
                return true;
            }
        }
        error = "Такой команды нет";
        return false;
    }

    #region command checkers methods
    private CheckerResult ShopGetProductListChecker(ParseResult parse)
    {
        if (parse.WhoWillHandle == "shop" && parse.HandleMethod == "getProductList")
        {
            return new(false, false);
        }
        return new(true, false);
    }

    private CheckerResult StoreGetProductListChecker(ParseResult parse)
    {
        if (parse.WhoWillHandle == "store" && parse.HandleMethod == "getProductList")
        {
            return new(false, false);
        }
        return new(true, false);
    }

    private CheckerResult ShopBuyChecker(ParseResult parse)
    {
        if (parse.WhoWillHandle == "shop" && parse.HandleMethod == "buy")
        {
            var hasNoArgsError = parse.Args.Length == 1 && parse.Args[0].All(char.IsDigit);
            return new(false, !hasNoArgsError);
        }
        return new(true, false);
    }

    private CheckerResult StoreOrderChecker(ParseResult parse)
    {
        if (parse.WhoWillHandle == "store" && parse.HandleMethod == "order")
        {
            var hasNoArgsError = parse.Args.Length == 1 && parse.Args[0].All(char.IsDigit);
            return new(false, !hasNoArgsError);
        }
        return new(true, false);
    }
    #endregion
}

public record ParseResult(string WhoWillHandle, string HandleMethod, string[] Args)
{

}

public record InputAnswer(bool IsSuccess, ParseResult? result, string? error)
{

}

public record CheckerResult(bool IsNotACommand, bool ArgsProblem)
{

}