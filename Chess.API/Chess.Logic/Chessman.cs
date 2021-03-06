﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Chess.Logic.Consts;
using Chess.Logic.Exceptions;
using Chess.Logic.Figures;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public abstract class Chessman
    {
        private readonly Color _color;
        public string CurrentLocation { get; set; }

        protected static readonly Dictionary<string, int> LocationToNumberMapper = new Dictionary<string, int>
        {
            {Locations.A8, 1}, {Locations.B8, 2}, {Locations.C8, 3}, {Locations.D8, 4}, {Locations.E8, 5}, {Locations.F8, 6}, {Locations.G8, 7}, {Locations.H8, 8},
            {Locations.A7, 9}, {Locations.B7, 10}, {Locations.C7, 11}, {Locations.D7, 12}, {Locations.E7,  13}, {Locations.F7, 14}, {Locations.G7, 15}, {Locations.H7, 16},
            {Locations.A6, 17}, {Locations.B6, 18}, {Locations.C6, 19}, {Locations.D6, 20}, {Locations.E6, 21}, {Locations.F6, 22}, {Locations.G6, 23}, {Locations.H6, 24},
            {Locations.A5, 25}, {Locations.B5, 26}, {Locations.C5, 27}, {Locations.D5, 28}, {Locations.E5, 29}, {Locations.F5, 30}, {Locations.G5, 31}, {Locations.H5, 32},
            {Locations.A4, 33}, {Locations.B4, 34}, {Locations.C4, 35}, {Locations.D4, 36}, {Locations.E4, 37}, {Locations.F4, 38}, {Locations.G4, 39}, {Locations.H4, 40},
            {Locations.A3, 41}, {Locations.B3, 42}, {Locations.C3, 43}, {Locations.D3, 44}, {Locations.E3, 45}, {Locations.F3, 46}, {Locations.G3, 47}, {Locations.H3, 48},
            {Locations.A2, 49}, {Locations.B2, 50}, {Locations.C2, 51}, {Locations.D2, 52}, {Locations.E2, 53}, {Locations.F2, 54}, {Locations.G2, 55}, {Locations.H2, 56},
            {Locations.A1, 57}, {Locations.B1, 58}, {Locations.C1, 59}, {Locations.D1, 60}, {Locations.E1, 61}, {Locations.F1, 62}, {Locations.G1, 63}, {Locations.H1, 64}
        };

        protected static readonly Dictionary<int, string> NumberToLocationMapper =
            LocationToNumberMapper.ToDictionary(kp => kp.Value, kp => kp.Key);

        protected static readonly Dictionary<char, int> CharToColumnNumberMapper = new Dictionary<char, int>
            {{'a', 8}, {'b', 7}, {'c', 6}, {'d', 5}, {'e', 4}, {'f', 3}, {'g', 2}, {'h', 1}};

        protected static readonly Dictionary<int, char> ColumnNumberToCharMapper =
            CharToColumnNumberMapper.ToDictionary(kp => kp.Value, kp => kp.Key);

        public Chessman(Color color, string currentLocation)
        {
            _color = color;
            CurrentLocation = currentLocation;
        }

        public Color GetColor()
        {
            return _color;
        }

        public abstract MoveResult Move(IBoard board, string to);

        public abstract bool CanAttackField(IBoard board, string to);

        public abstract IEnumerable<Move> GetPossibleMoves(IBoard board);

        protected (MoveStatus status, Chessman captured) RecognizeMoveType(IBoard board, string to)
        {
            if (board.GetChessman(to) != null)
            {
                return (MoveStatus.Capture, board.GetChessman(to));
            }

            return (MoveStatus.Normal, null);
        }

        protected virtual void MoveToDestination(IBoard board, string to)
        {
            var captured = board.GetChessman(to);
            if (captured != null)
            {
                board.GetPlayerFigures(captured.GetColor()).Remove(captured);
            }

            board.SetChessman(to, this);
            board.SetChessman(CurrentLocation, null);
            CurrentLocation = to;          
        }

        protected bool LocationNumberExists(int locationNumber)
        {
            return NumberToLocationMapper.ContainsKey(locationNumber);
        }

        protected string ConvertFigure(Chessman chessman)
        {
            switch (chessman?.GetType().Name)
            {
                case nameof(Bishop):
                    return Figure.Bishop;
                case nameof(King):
                    return Figure.King;
                case nameof(Queen):
                    return Figure.Queen;
                case nameof(Rook):
                    return Figure.Rook;
                case nameof(Pawn):
                    return Figure.Pawn;
                case nameof(Knight):
                    return Figure.Knight;
                default:
                    return null;
            }
        }
    }
}