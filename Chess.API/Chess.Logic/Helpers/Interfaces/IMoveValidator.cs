using System.Collections.Generic;

namespace Chess.Logic.Helpers.Interfaces
{
    public interface IMoveValidator
    {
        bool IsMoveValid(IEnumerable<int> availableMoves, string @from, string to);
    }
}