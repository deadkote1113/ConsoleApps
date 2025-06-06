namespace TicTacToe.Game.Logic;

public class ScreenDrawer
{
    public void Draw(string board, Player playingPlayer, string? error = null)
    {
        Console.Clear();
        Console.WriteLine($"сейчас ходит игрок {playingPlayer}");
        if (string.IsNullOrEmpty(error) == false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine(board);
    }
}
