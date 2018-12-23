using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Logic.Exceptions;
using Chess.Logic.Figures;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public class Board : IBoard
    {
        private readonly IDictionary<string, Chessman> _board;
        private readonly List<Chessman> _blackFigures;
        private readonly List<Chessman> _whiteFigures;

        public Board()
        {
            _board = new Dictionary<string, Chessman>
            {
                {Locations.A8, new Rook(Color.Black, Locations.A8)}, {Locations.B8, new Knight(Color.Black, Locations.B8)}, {Locations.C8, new Bishop(Color.Black, Locations.C8)}, {Locations.D8, new Queen(Color.Black, Locations.D8)}, {Locations.E8, new King(Color.Black, Locations.E8)}, {Locations.F8, new Bishop(Color.Black, Locations.F8)}, {Locations.G8, new Knight(Color.Black, Locations.G8)}, {Locations.H8, new Rook(Color.Black, Locations.H8)},
                {Locations.A7, new Pawn(Color.Black, Locations.A7)}, {Locations.B7,  new Pawn(Color.Black, Locations.B7)}, {Locations.C7,  new Pawn(Color.Black, Locations.C7)}, {Locations.D7, new Pawn(Color.Black, Locations.D7)}, {Locations.E7,  new Pawn(Color.Black, Locations.E7)}, {Locations.F7, new Pawn(Color.Black, Locations.F7)}, {Locations.G7, new Pawn(Color.Black, Locations.G7)}, {Locations.H7,  new Pawn(Color.Black, Locations.H7)},
                {Locations.A6, null}, {Locations.B6, null}, {Locations.C6, null}, {Locations.D6, null}, {Locations.E6, null}, {Locations.F6, null}, {Locations.G6, null}, {Locations.H6, null},
                {Locations.A5, null}, {Locations.B5, null}, {Locations.C5, null}, {Locations.D5, null}, {Locations.E5, null}, {Locations.F5, null}, {Locations.G5, null}, {Locations.H5, null},
                {Locations.A4, null}, {Locations.B4, null}, {Locations.C4, null}, {Locations.D4, null}, {Locations.E4, null}, {Locations.F4, null}, {Locations.G4, null}, {Locations.H4, null},
                {Locations.A3, null}, {Locations.B3, null}, {Locations.C3, null}, {Locations.D3, null}, {Locations.E3, null}, {Locations.F3, null}, {Locations.G3, null}, {Locations.H3, null},
                {Locations.A2, new Pawn(Color.White, Locations.A2)}, {Locations.B2, new Pawn(Color.White, Locations.B2)}, {Locations.C2, new Pawn(Color.White, Locations.C2)}, {Locations.D2, new Pawn(Color.White, Locations.D2)}, {Locations.E2, new Pawn(Color.White, Locations.E2)}, {Locations.F2, new Pawn(Color.White, Locations.F2)}, {Locations.G2, new Pawn(Color.White, Locations.G2)}, {Locations.H2, new Pawn(Color.White, Locations.H2)},
                {Locations.A1, new Rook(Color.White, Locations.A1)}, {Locations.B1, new Knight(Color.White, Locations.B1)}, {Locations.C1, new Bishop(Color.White, Locations.C1)}, {Locations.D1, new Queen(Color.White, Locations.D1)}, {Locations.E1, new King(Color.White, Locations.E1)}, {Locations.F1, new Bishop(Color.White, Locations.F1)}, {Locations.G1, new Knight(Color.White, Locations.G1)}, {Locations.H1, new Rook(Color.White, Locations.H1)}
            };

            _blackFigures = _board.Values.Where(x => x !=null && x.GetColor() == Color.Black).ToList();
            _whiteFigures = _board.Values.Where(x => x != null && x.GetColor() == Color.White).ToList();
        }


        public Chessman GetChessman(string location)
        {
            return _board.ContainsKey(location) ? _board[location] : throw new InvalidFieldException();
        }

        public T GetChessman<T>(Color color) where T : Chessman
        {
            return color == Color.White
                ? _whiteFigures.Single(x => x.GetType() == typeof(T)) as T
                : _blackFigures.Single(x => x.GetType() == typeof(T)) as T;
        }

        public bool IsKingInCheck(Color color)
        {
            return IsFieldAttacked(GetChessman<King>(color).CurrentLocation, color);
        }

        public void RemoveChessman(Chessman chessman)
        {
            var figures = chessman.GetColor() == Color.White ? _whiteFigures : _blackFigures;
            if (!figures.Remove(chessman))
            {
                throw new RemoveChessmanException($"{chessman.GetColor()} {typeof(Chessman)} not found on the board");
            }
        }

        public IEnumerable<Chessman> GetPlayerFigures(Color color)
        {
            return color == Color.White ? _whiteFigures : _blackFigures;
        }

        public Type GetChessmanType(string location)
        {
            if (!_board.ContainsKey(location))
            {
                throw new InvalidFieldException();
            }

            return _board[location]?.GetType();
        }

        public void SetChessman(string location, Chessman chessman)
        {
            _board[location] = chessman;
        }

        public bool FieldExists(string field)
        {
            return _board.ContainsKey(field);
        }

        public bool IsFieldAttacked(string field, Color figureColor)
        {
            if (_board.ContainsKey(field))
            {
                var opponentFigures = figureColor == Color.White ? _blackFigures : _whiteFigures;
                return opponentFigures.Count(x => x.CanAttackField(this, field)) > 0;
            }

            throw new InvalidFieldException();
        }
    }
}