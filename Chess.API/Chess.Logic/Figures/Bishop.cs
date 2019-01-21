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
            if (!IsMoveValid(board, to))
            {
                throw new InvalidMoveException($"{GetType().Name} cannot make move: {CurrentLocation}:{to}");
            }

            var from = CurrentLocation;
            var moveType = RecognizeMoveType(board, to);      
            MoveToDestination(board, to);

            if (board.IsKingInCheck(GetColor()))
            {
                MoveToDestination(board, from);
                if (moveType.status == MoveStatus.Capture)
                {
                    board.SetChessman(to, moveType.captured);
                    board.GetPlayerFigures(moveType.captured.GetColor()).Add(moveType.captured);
                }
                throw new InvalidMoveException($"{GetType().Name} cannot make move: {CurrentLocation}:{to} - move leaves friendly king in check");
            }

            return new MoveResult(Figure.Bishop, from, to, moveType.status, GetColor(), ConvertFigure(moveType.captured));
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
            var leftUpCornerNumber = GetLeftTopCornerNumber(CurrentLocation);
            var rightUpCornerNumber = GetRightTopCornerNumber(CurrentLocation);
            var leftDownCornerNumber = GetLeftDownCornerNumber(CurrentLocation);
            var rightDownCornerNumber = GetRightDownCornerNumber(CurrentLocation);
            var possibleFields = GetFieldsInDirection(leftUpCornerNumber, -9).
                Concat(GetFieldsInDirection(rightUpCornerNumber, -7))
                .Concat(GetFieldsInDirection(leftDownCornerNumber, 7))
                .Concat(GetFieldsInDirection(rightDownCornerNumber, 9));

            foreach (var field in possibleFields)
            {
                if (IsMoveValid(board, field))
                {
                    possibleMoves.Add(new Move(this, CurrentLocation, field));
                }
            }

            return possibleMoves;
        }

        private bool IsMoveValid(IBoard board, string to)
        {
            if (board.GetChessman(to)?.GetColor() == GetColor())
            {
                return false;
            }

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

        private IEnumerable<string> GetFieldsInDirection(int destinationNumber, int directionIndicator)
        {
            List<string> possibleFields = new List<string>();
            var currentLocationNumber = LocationToNumberMapper[CurrentLocation];
            while (destinationNumber != (currentLocationNumber + directionIndicator))
            {
                possibleFields.Add(NumberToLocationMapper[currentLocationNumber+directionIndicator]);
                currentLocationNumber += directionIndicator;
            }

            return possibleFields;
        }

        private int GetLeftTopCornerNumber(string from)
        {
            var columnFrom = CharToColumnNumberMapper[from[0]];
            var rowFrom = (int)Char.GetNumericValue(from[1]);

            int fields = 8 - rowFrom;
            //if bishop is above diagonal line 1 (diagonal from a8-h1)
            if (rowFrom>columnFrom)
            {
                return LocationToNumberMapper[ColumnNumberToCharMapper[columnFrom + fields] + "8"];
            }

            fields = 8 - columnFrom;
            var rowTo = rowFrom + fields;
            return LocationToNumberMapper["a" + rowTo];
        }

        private int GetRightTopCornerNumber(string from)
        {
            var columnFrom = CharToColumnNumberMapper[from[0]];
            var rowFrom = (int)Char.GetNumericValue(from[1]);

            int fields;
            //if bishop is above diagonal line 2 (diagonal from a1-h8)
            if (Math.Abs(1-rowFrom)>Math.Abs(8-columnFrom))
            {
                fields = 8 - rowFrom;
                return LocationToNumberMapper[ColumnNumberToCharMapper[columnFrom - fields] + "8"];
            }

            fields = Math.Abs(1 - columnFrom);
            var rowTo = rowFrom - fields;
            return LocationToNumberMapper["h" + rowTo];
        }

        private int GetLeftDownCornerNumber(string from)
        {
            var columnFrom = CharToColumnNumberMapper[from[0]];
            var rowFrom = (int)Char.GetNumericValue(from[1]);

            int fields = Math.Abs(1-rowFrom);
            //if bishop is below or inline with diagonal line 2 (diagonal from a1-h8)
            if (Math.Abs(1 - rowFrom) <= Math.Abs(8 - columnFrom))
            {
                return LocationToNumberMapper[ColumnNumberToCharMapper[columnFrom + fields] + "1"];
            }

            fields = Math.Abs(8 - columnFrom);
            var rowTo = rowFrom - fields;
            return LocationToNumberMapper["a" + rowTo];
        }

        private int GetRightDownCornerNumber(string from)
        {
            var columnFrom = CharToColumnNumberMapper[from[0]];
            var rowFrom = (int)Char.GetNumericValue(from[1]);

            int fields = Math.Abs(1 - rowFrom);
            //if bishop is below or inline with diagonal line 2 (diagonal from a1-h8)
            if (Math.Abs(1 - rowFrom) <= Math.Abs(8 - columnFrom))
            {
                return LocationToNumberMapper[ColumnNumberToCharMapper[columnFrom - fields] + "1"];
            }

            fields = Math.Abs(1 - columnFrom);
            var rowTo = rowFrom - fields;
            return LocationToNumberMapper["h" + rowTo];
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