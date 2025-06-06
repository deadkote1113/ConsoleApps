namespace TicTacToe.Game.Logic;

public class ConsoleInputHandler
{
    private readonly List<int> _usedValues = [];

    public InputResult Handle()
    {
        var isNumber = int.TryParse(Console.ReadLine(), out var number);

        if (isNumber == false)
            return new(false, "введите число", null, null);
        if (number < 1 || number > 9)
            return new(false, "число должно быть в диапазоне между 1 и 9", null, null);
        if (_usedValues.Contains(number))
            return new(false, "клетка уже занята", null, null);

        _usedValues.Add(number);
        return new(true, null, (number - 1) % 3, (number - 1) / 3);
    }
}

public record InputResult(bool IsSuccess, string? Error, int? X, int? Y)
{

}