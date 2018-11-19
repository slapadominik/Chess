﻿using System;
using System.Collections.Generic;
using System.Threading;
using Chess.Logic.Exceptions;
using Chess.Logic.Figures;
using Chess.Logic.Interfaces;

namespace Chess.Logic
{
    public class Game : IGame
    {
        private IBoard _board;

        private Player _playerWhite;
        private Player _playerBlack;
        private Player _currentPlayer;
        private int _moves;
        private Guid _id;
        private bool _gameStarted;

        public Game(Guid playerWhite, Guid playerBlack, IBoard board)
        {
            _id = Guid.NewGuid();
            _playerWhite = new Player(playerWhite, Color.White);
            _playerBlack = new Player(playerBlack, Color.Black);
            _moves = 0;
            _currentPlayer = _playerWhite;
            _board = board;
        }

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
        }


        public int MovesCount()
        {
            return _moves;
        }

        public void StartGame()
        {
            _gameStarted = true;
        }

        public Guid GetId()
        {
            return _id;
        }

        public MoveStatus MakeMove(Guid playerId, string @from, string to)
        {
            if (!_gameStarted)
            {
                throw new GameNotStartedException($"Cannot make move in Game [{_id}] - game hasn't started yet.");
            }

            if (!IsCurrentPlayer(playerId))
            {
                throw new NotACurrentPlayerException($"Game {_id}: Player {playerId} is not a current player.");
            }

            CheckIfFieldsExist(from, to);

            var figure = _board.GetChessman(@from);
            if (figure == null)
            {
                throw new EmptyLocationException($"Game {_id}: Location {@from} doesn't contain chessman.");
            }

            if (figure.GetColor() != _currentPlayer.Color)
            {
                throw new WrongPlayerChessmanException(
                    $"Game {_id}: Player {_currentPlayer.Color} can't move Chessman {figure.GetColor()}");
            }

            try
            {
                var moveResult = figure.MakeMove(_board, from, to);
                _moves++;
                _currentPlayer = SetCurrentPlayer();
                return moveResult.MoveStatus;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CheckIfFieldsExist(string from, string to)
        {
            if (_board.FieldExists(from) && _board.FieldExists(to))
            {
                throw new InvalidFieldException();
            }
        }

        private Player SetCurrentPlayer()
        {
            return _moves % 2 == 0 ? _playerWhite: _playerBlack;
        }

        private bool IsCurrentPlayer(Guid id)
        {
            return _currentPlayer.Id == id;
        }

       
    }
}