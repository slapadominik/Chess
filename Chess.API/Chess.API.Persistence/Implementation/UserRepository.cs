using System;
using System.Collections.Generic;
using System.Linq;
using Chess.API.Persistence.DAO;
using Chess.API.Persistence.Interfaces;

namespace Chess.API.Persistence.Implementation
{
    public class UserRepository : IUserRepository
    {
        private static List<UserDAO> _users;

        public UserRepository()
        {
            _users = new List<UserDAO>();
        }
        public void AddUser(UserDAO user)
        {
            _users.Add(user);
        }

        public UserDAO GetUserById(Guid id)
        {
           return _users.Single(x => x.Id == id);
        }
    }
}