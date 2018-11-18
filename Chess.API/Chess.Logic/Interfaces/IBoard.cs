using System.Collections.Generic;

namespace Chess.Logic.Interfaces
{
    public interface IBoard
    {
        Chessman GetChessman(string location);
        void SetChessman(string location, Chessman chessman);
        bool FieldExists(string field);
        bool IsMoveValid(IEnumerable<int> availableMoves, string @from, string to);
    }
}