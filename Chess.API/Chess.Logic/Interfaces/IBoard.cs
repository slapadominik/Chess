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
    }
}