using System;
using System.Collections.Generic;
using Chess.Logic.Consts;

namespace Chess.Logic.Interfaces
{
    public interface IBoard
    {
        Chessman GetChessman(string location);
        Type GetChessmanType(string location);
        void SetChessman(string location, Chessman chessman);
        bool FieldExists(string field);
        bool IsFieldAttacked(string field, Color color);
        T GetChessman<T>(Color color) where T : Chessman;
        bool IsKingInCheck(Color color);
        void RemoveChessman(Chessman chessman);
        IEnumerable<Chessman> GetPlayerFigures(Color color);
    }
}