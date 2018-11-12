using System;

namespace Chess.API.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        private static int _counter = 0;

        public User()
        {
            _counter++;
            Username = "User" + _counter;
            Id = Guid.NewGuid();
        }

        public User(Guid id, string username)
        {
            Username = username;
            Id = id;
        }
    }
}