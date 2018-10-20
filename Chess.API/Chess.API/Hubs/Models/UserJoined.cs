using System;

namespace Chess.API.Hubs.Models
{
    public class UserJoined
    {
        public Guid Id { get; }
        public UserJoined(Guid id)
        {
            Id = id;
        }
    }
}