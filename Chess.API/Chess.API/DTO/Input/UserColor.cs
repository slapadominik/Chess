using System;
using Chess.Logic;


namespace Chess.API.DTO.Input
{
    public class UserColor
    {
        public Guid Id { get; set; }
        public Color Color { get; set; }
    }
}