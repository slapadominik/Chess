using System;

namespace Chess.API.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}