namespace ShopAndStore.Logic;

public static class DateExtension
{
    public static bool IsHolliday(this DateOnly date)
    {
        return date.DayOfWeek switch
        {
            DayOfWeek.Sunday => true,
            DayOfWeek.Saturday => true,
            _ => false
        };
    }

    public static bool IsOdd(this DateOnly date)
    {
        return date.Day % 2 == 1;
    }
}
