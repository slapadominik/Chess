using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Chess.Logic.Consts;
using Chess.Logic.Exceptions;
using Chess.Logic.Interfaces;

namespace Chess.Logic.Figures
{
    public class Bishop : Chessman
    {

        public Bishop(Color color, string currentLocation) : base(color, currentLocation)
        {
        }

        public override MoveResult Move(IBoard board, string to)
        {
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                throw new InvalidMoveException($"Location [{to}] contains friendly chessman!");
            }      
            
            if (!IsMoveValid(board, to))
            {
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to}");
            }


            var from = CurrentLocation;
            var moveType = RecognizeMoveType(board, to);      
            MoveToDestination(board, to);

            if (board.IsKingInCheck(GetColor()))
            {
                MoveToDestination(board, from);
                throw new InvalidMoveException($"{GetType()} cannot make move: {CurrentLocation}:{to} - move leaves friendly king in check");
            }

            return new MoveResult(from, to, moveType.status, GetColor(), moveType.captured);
        }

        public override bool CanAttackField(IBoard board, string to)
        {
            if (to.Equals(CurrentLocation))
            {
                return false;
            }

            return IsDiagonalMove(to) && IsDiagonalMoveLegal(board, to);
        }

        public override IEnumerable<Move> GetPossibleMoves(IBoard board)
        {
            var possibleMoves = new List<Move>();
            var leftCorner = GetLastUpLeftDiagonalLocation(CurrentLocation);

            CanAttackField(board, leftCorner);
            var from = CurrentLocation;

            while (CanAttackField(board, from))
            {
                
            }

            return possibleMoves;
        }

        private bool IsMoveValid(IBoard board, string to)
        {
            if (typeof(King) == (board.GetChessmanType(to)))
            {
                return false;
            }

            return IsDiagonalMove(to) && IsDiagonalMoveLegal(board, to);
        }

        private bool IsDiagonalMove(string to)
        {
            return Math.Abs(CharToColumnNumberMapper[to[0]] - CharToColumnNumberMapper[CurrentLocation[0]]) == Math.Abs(to[1] - CurrentLocation[1]);
        }

        private bool IsDiagonalMoveLegal(IBoard board, string to)
        {
            var rowTo = (int)Char.GetNumericValue(to[1]);
            var column = CharToColumnNumberMapper[CurrentLocation[0]];
            var rowFrom = (int)Char.GetNumericValue(CurrentLocation[1]);
            var fieldsOnPath = Math.Abs(rowTo - rowFrom);
            var columnIndicator = (CharToColumnNumberMapper[to[0]] - column) / fieldsOnPath;
            var rowIndicator = (rowTo - rowFrom) / fieldsOnPath;

            for (int i = 0; i < fieldsOnPath - 1; i++)
            {
                rowFrom += rowIndicator;
                column += columnIndicator;
                if (board.GetChessman(ColumnNumberToCharMapper[column] + (rowFrom).ToString()) != null)
                {
                    return false;
                }
            }

            return true;
        }

        private string GetLastUpLeftDiagonalLocation(string from)
        {
            var columnFrom = CharToColumnNumberMapper[from[0]];
            var rowFrom = (int)Char.GetNumericValue(from[1]);
            if (columnFrom == 1 || rowFrom == 8)
            {
                return from;
            }

            int fields;
            if (columnFrom >= 4)
            {
                fields = 8 - rowFrom;
                return ColumnNumberToCharMapper[columnFrom + fields] + "8";
            }

            fields = Math.Abs(1 - columnFrom);
            columnFrom += fields;
            return "a" + columnFrom;
        }

        private string GetLastUpRightDiagonalLocation(string from)
        {
            var columnFrom = CharToColumnNumberMapper[from[0]];
            var rowFrom = (int)Char.GetNumericValue(from[1]);
            if (columnFrom == 1 || rowFrom == 8)
            {
                return from;
            }

            int fields;
            if (columnFrom >= 4)
            {
                fields = 8 - rowFrom;
                return ColumnNumberToCharMapper[columnFrom + fields] + "8";
            }

            fields = Math.Abs(1 - columnFrom);
            columnFrom += fields;
            return "a" + columnFrom;
        }

        private string GetLastDownLeftDiagonalLocation(string from)
        {
            var columnFrom = CharToColumnNumberMapper[from[0]];
            var rowFrom = (int)Char.GetNumericValue(from[1]);
            if (columnFrom == 1 || rowFrom == 8)
            {
                return from;
            }

            int fields;
            if (columnFrom >= 4)
            {
                fields = 8 - rowFrom;
                return ColumnNumberToCharMapper[columnFrom + fields] + "8";
            }

            fields = Math.Abs(1 - columnFrom);
            columnFrom += fields;
            return "a" + columnFrom;
        }

        private string GetLastDownRightDiagonalLocation(string from)
        {
            var columnFrom = CharToColumnNumberMapper[from[0]];
            var rowFrom = (int)Char.GetNumericValue(from[1]);
            if (columnFrom == 1 || rowFrom == 8)
            {
                return from;
            }

            int fields;
            if (columnFrom >= 4)
            {
                fields = 8 - rowFrom;
                return ColumnNumberToCharMapper[columnFrom + fields] + "8";
            }

            fields = Math.Abs(1 - columnFrom);
            columnFrom += fields;
            return "a" + columnFrom;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Bishop)
            {
                return CurrentLocation.Equals(((Bishop)obj).CurrentLocation);
            }

            return false;
        }

        public override string ToString()
        {
            switch (GetColor())
            {
                case Color.White:
                    return "w" + Figure.Bishop;
                case Color.Black:
                    return "b" + Figure.Bishop;
                default:
                    throw new InvalidEnumArgumentException();
            }
           
        }
    }
}