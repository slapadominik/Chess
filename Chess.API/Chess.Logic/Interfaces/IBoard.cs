﻿namespace Chess.Logic.Interfaces
{
    public interface IBoard
    {
        Chessman GetChessman(string location);
        void SetChessman(string location, Chessman chessman);
    }
}