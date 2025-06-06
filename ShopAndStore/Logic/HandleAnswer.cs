namespace ShopAndStore.Logic;

public record HandleAnswer(bool IsSuccess, HandlerResult? Response, string? Error)
{

}

public record HandlerResult(string TextResponse, decimal Profit, bool IsDayEnds)
{

}