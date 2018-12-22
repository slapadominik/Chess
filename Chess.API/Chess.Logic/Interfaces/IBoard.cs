using System;
using System.Collections.Generic;

namespace Chess.Logic.Interfaces
{
    public interface IBoard
    {
        Chessman GetChessman(string location);
        Type GetChessmanType(string location);
        void SetChessman(string location, Chessman chessman);
        bool FieldExists(string field);
        bool IsFieldAttacked(string field, Color color);
        Chessman GetChessman<T>(Color color) where T : Chessman;
        void RemoveChessman(Chessman chessman);
    }
}