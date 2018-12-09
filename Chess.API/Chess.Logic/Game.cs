using System;
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

        public void StartGame(Guid userId)
        {
            if (_playerBlack.Id != userId && _playerWhite.Id != userId)
            {
                throw new InvalidOperationException($"User {userId} is not a player in game {_id} - he cannot start game.");
            }
            _gameStarted = true;
        }

        public bool IsGameStarted()
        {
            return _gameStarted;
        }

        public Guid GetId()
        {
            return _id;
        }

        public MoveResult MakeMove(Guid playerId, string @from, string to)
        {
            if (!_gameStarted)
            {
                throw new GameNotStartedException($"Cannot make move in Game [{_id}] - game hasn't started yet.");
            }

            if (!IsCurrentPlayer(playerId))
            {
                throw new NotACurrentPlayerException($"Game {_id}: Player {playerId} is not a current player.");
            }

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

            var moveResult = figure.Move(_board, to);
            _moves++;
            _currentPlayer = SetCurrentPlayer();
            return moveResult;
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