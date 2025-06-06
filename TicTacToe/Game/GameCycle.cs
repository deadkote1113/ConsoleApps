using TicTacToe.Game.Enum;
using TicTacToe.Game.Logic;

namespace TicTacToe.Game;

public class GameCycle
{
    private readonly ScreenDrawer _screenDrawer = new();
    private readonly ConsoleInputHandler _consoleInputHandler = new();
    private readonly GameBoard _gameBoard = new();

    private Player _playingPlayer = Player.First;
    private Cell CurrentCell => _playingPlayer switch
    {
        Player.First => Cell.X,
        Player.Second => Cell.O,
        _ => throw new ArgumentException()
    };

    public void StartGame()
    {
        while (true)
        {
            DrawScreen();
            var (x, y) = AwaitSuccessInput();
            _gameBoard.ChangeValue(x, y, CurrentCell);
            if (_gameBoard.IsGameEnd())
                break;
            EndTurn();
        }

        DrawScreen();
        Console.WriteLine($"победил {_playingPlayer} игрок");
    }

    private (int x, int y) AwaitSuccessInput()
    {
        while (true)
        {
            var inputResult = _consoleInputHandler.Handle();

            if (inputResult.IsSuccess)
                return (inputResult.X!.Value, inputResult.Y!.Value);
            else
                DrawScreen(inputResult.Error);
        }
    }

    private void DrawScreen(string? error = null)
    {
        _screenDrawer.Draw(_gameBoard.GetBoard(), _playingPlayer, error);
    }

    private void EndTurn()
    {
        _playingPlayer = _playingPlayer == Player.First ? Player.Second : Player.First;
    }
}