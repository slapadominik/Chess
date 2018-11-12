using System;
using System.Collections;
using System.Collections.Generic;
using Chess.API.Persistence.DAO;

namespace Chess.API.Persistence.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(UserDAO user);
        UserDAO GetUserById(Guid id);
        IEnumerable<UserDAO> GetUsers();
    }
}