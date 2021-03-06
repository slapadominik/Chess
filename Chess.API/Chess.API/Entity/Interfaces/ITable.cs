﻿using System;
using System.Threading.Tasks;
using Chess.Logic;
using Chess.Logic.Consts;
using Chess.Logic.Interfaces;

namespace Chess.API.Entity.Interfaces
{
    public interface ITable
    {
        int Number { get; }
        IGame Game { get; set; }
        Guid PlayerWhiteId { get; }
        Guid PlayerBlackId { get; }

        bool PlayerBlackOccupied();
        bool PlayerWhiteOccupied();
        bool IsPlayer(Guid userId);
        void JoinTable(Guid playerId, Color color);
        bool IsFull();
        void DismissPlayers();
    }
}