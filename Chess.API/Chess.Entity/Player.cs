using System;
using System.Collections.Generic;

namespace Chess.Entity
{
    public class Player
    {
        public Guid Id { get; set; }
        public List<Chessman> Chessmen { get; set; }
    }
}