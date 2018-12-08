using System.Collections.Generic;
using Chess.Logic.Exceptions;
using Chess.Logic.Figures;
using Chess.Logic.Helpers.Interfaces;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public class Board : IBoard
    {
        private readonly IDictionary<string, Chessman> _board;
        private readonly IMoveValidator _moveValidator;

        public Board(IMoveValidator moveValidator)
        {
            _board = new Dictionary<string, Chessman>
            {
                {Locations.A8, new Rook(Color.Black)}, {Locations.B8, new Knight(Color.Black)}, {Locations.C8, new Bishop(Color.Black)}, {Locations.D8, new Queen(Color.Black)}, {Locations.E8, new King(Color.Black)}, {Locations.F8, new Bishop(Color.Black)}, {Locations.G8, new Knight(Color.Black)}, {Locations.H8, new Rook(Color.Black)},
                {Locations.A7, new Pawn(Color.Black)}, {Locations.B7,  new Pawn(Color.Black)}, {Locations.C7,  new Pawn(Color.Black)}, {Locations.D7, new Pawn(Color.Black)}, {Locations.E7,  new Pawn(Color.Black)}, {Locations.F7, new Pawn(Color.Black)}, {Locations.G7, new Pawn(Color.Black)}, {Locations.H7,  new Pawn(Color.Black)},
                {Locations.A6, null}, {Locations.B6, null}, {Locations.C6, null}, {Locations.D6, null}, {Locations.E6, null}, {Locations.F6, null}, {Locations.G6, null}, {Locations.H6, null},
                {Locations.A5, null}, {Locations.B5, null}, {Locations.C5, null}, {Locations.D5, null}, {Locations.E5, null}, {Locations.F5, null}, {Locations.G5, null}, {Locations.H5, null},
                {Locations.A4, null}, {Locations.B4, null}, {Locations.C4, null}, {Locations.D4, null}, {Locations.E4, null}, {Locations.F4, null}, {Locations.G4, null}, {Locations.H4, null},
                {Locations.A3, null}, {Locations.B3, null}, {Locations.C3, null}, {Locations.D3, null}, {Locations.E3, null}, {Locations.F3, null}, {Locations.G3, null}, {Locations.H3, null},
                {Locations.A2, new Pawn(Color.White)}, {Locations.B2, new Pawn(Color.White)}, {Locations.C2, new Pawn(Color.White)}, {Locations.D2, new Pawn(Color.White)}, {Locations.E2, new Pawn(Color.White)}, {Locations.F2, new Pawn(Color.White)}, {Locations.G2, new Pawn(Color.White)}, {Locations.H2, new Pawn(Color.White)},
                {Locations.A1, new Rook(Color.White)}, {Locations.B1, new Knight(Color.White)}, {Locations.C1, new Bishop(Color.White)}, {Locations.D1, new Queen(Color.White)}, {Locations.E1, new King(Color.White)}, {Locations.F1, new Bishop(Color.White)}, {Locations.G1, new Knight(Color.White)}, {Locations.H1, new Rook(Color.White)}
            };
            _moveValidator = moveValidator;
        }


        public Chessman GetChessman(string location)
        {
            return _board.ContainsKey(location) ? _board[location] : throw new InvalidFieldException();
        }

        public void SetChessman(string location, Chessman chessman)
        {
            _board[location] = chessman;
        }

        public bool FieldExists(string field)
        {
            return _board.ContainsKey(field);
        }

        public bool IsMoveValid(IEnumerable<int> availableMoves, string @from, string to)
        {
            return _moveValidator.IsMoveValid(availableMoves, from, to);
        }
    }
}