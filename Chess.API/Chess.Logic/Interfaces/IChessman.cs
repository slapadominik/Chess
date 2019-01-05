﻿using Chess.Logic.Consts;

namespace Chess.Logic.Interfaces
{
    public interface IChessman
    {
        Color GetColor();
        string GetFigure();
    }
}