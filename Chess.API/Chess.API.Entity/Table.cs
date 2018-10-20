using System;
using Chess.Entity;

namespace Chess.API.Entity
{
    public class Table
    {
        public Guid Id { get; set; }
        public User Operator { get; set; }
        public Game Game { get; set; }
    }
}