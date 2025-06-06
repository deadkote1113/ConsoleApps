namespace ShopAndStore.Logic.InputOutput;

public class InputHandler
{
    private readonly InputParser _inputParser = new();

    public InputAnswer Handle()
    {
        var input = Console.ReadLine() ?? "";
        return _inputParser.Parse(input);
    }
}
