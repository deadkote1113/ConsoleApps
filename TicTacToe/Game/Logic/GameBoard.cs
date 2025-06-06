using System.Text;
using TicTacToe.Game.Enum;

namespace TicTacToe.Game.Logic;

public class GameBoard
{
    private const string DelimiterLine = "-------------------";

    private const string Emptiness1 = "     ";
    private static Func<int, string> Emptiness2 => (x) => $"  {x}  ";
    private const string Emptiness3 = "     ";

    private const string X1 = "*   *";
    private const string X2 = "  *  ";
    private const string X3 = "*   *";

    private const string O1 = " *** ";
    private const string O2 = "*   *";
    private const string O3 = " *** ";

    private Cell[][] _board =
    {
        [ Cell.Empty, Cell.Empty, Cell.Empty ],
        [ Cell.Empty, Cell.Empty, Cell.Empty ],
        [ Cell.Empty, Cell.Empty, Cell.Empty ],
    };

    public void ChangeValue(int x, int y, Cell value)
    {
        if (x < 0 || x > 2 || y < 0 || y > 2)
        {
            throw new Exception("wrong x or y");
        }
        if (_board[y][x] == value)
        {
            throw new Exception("old value equals new value");
        }
        _board[y][x] = value;
    }

    public bool IsGameEnd()
    {
        // горизонтали
        if (_board[0].All(item => item == Cell.X) || _board[0].All(item => item == Cell.O)
            ||
            _board[1].All(item => item == Cell.X) || _board[1].All(item => item == Cell.O)
            ||
            _board[2].All(item => item == Cell.X) || _board[2].All(item => item == Cell.O))
        {
            return true;
        }
        //вертикали
        if (_board.All(item => item.Skip(0).First() == Cell.X) || _board.All(item => item.Skip(0).First() == Cell.O)
            ||
            _board.All(item => item.Skip(1).First() == Cell.X) || _board.All(item => item.Skip(1).First() == Cell.O)
            ||
            _board.All(item => item.Skip(2).First() == Cell.X) || _board.All(item => item.Skip(2).First() == Cell.O))
        {
            return true;
        }
        //кресты
        if (_board[0][0] == _board[1][1] && _board[1][1] == _board[2][2] && _board[1][1] != Cell.Empty
            ||
            _board[0][2] == _board[1][1] && _board[1][1] == _board[2][0] && _board[1][1] != Cell.Empty)
        {
            return true;
        }
        return false;
    }

    public string GetBoard()
    {
        var result = new StringBuilder();
        for (int i = 0; i < 3; i++)
        {
            result.AppendLine(DelimiterLine);
            result.AppendLine($"|{GetBoardPart(0, i, 1)}|{GetBoardPart(1, i, 1)}|{GetBoardPart(2, i, 1)}|");
            result.AppendLine($"|{GetBoardPart(0, i, 2)}|{GetBoardPart(1, i, 2)}|{GetBoardPart(2, i, 2)}|");
            result.AppendLine($"|{GetBoardPart(0, i, 3)}|{GetBoardPart(1, i, 3)}|{GetBoardPart(2, i, 3)}|");
        }
        result.AppendLine(DelimiterLine);
        return result.ToString();
    }

    private string GetBoardPart(int x, int y, int part)
    {
        switch (_board[y][x])
        {
            case Cell.Empty:
                return part switch
                {
                    1 => Emptiness1,
                    2 => Emptiness2(y * 3 + x + 1),
                    3 => Emptiness3,
                    _ => throw new ArgumentException()
                };
            case Cell.X:
                return part switch
                {
                    1 => X1,
                    2 => X2,
                    3 => X3,
                    _ => throw new ArgumentException()
                };
            case Cell.O:
                return part switch
                {
                    1 => O1,
                    2 => O2,
                    3 => O3,
                    _ => throw new ArgumentException()
                };
        }
        throw new ArgumentException();
    }
}