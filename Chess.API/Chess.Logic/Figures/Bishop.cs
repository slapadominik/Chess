﻿using System;
using System.Collections.Generic;
using System.Linq;
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

            return new MoveResult(from, to, moveType.status, GetColor(), moveType.captured);
        }

        public override bool CanAttackField(IBoard board, string to)
        {
            return IsMoveValid(board, to);
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

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

    }
}