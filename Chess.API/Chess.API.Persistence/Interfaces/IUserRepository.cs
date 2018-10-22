using System;
using Chess.API.Persistence.DAO;

namespace Chess.API.Persistence.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(UserDAO user);
        UserDAO GetUserById(Guid id);
    }
}