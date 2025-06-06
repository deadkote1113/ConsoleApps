namespace ShopAndStore.Logic.InputOutput;

public class ConsoleOutput
{
    public void Write(string screan, string? error = null)
    {
        if (!string.IsNullOrEmpty(error))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine(screan);
    }
}
