using System;

namespace Chess.API.Entity
{
    public class User
    {
        public User(Guid id)
        {
            Id = id;
        }

        public User()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}