using System;
using System.Collections;
using System.Collections.Generic;
using Chess.API.Entity;

namespace Chess.API.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(Guid id);
        IEnumerable<User> GetUsers();
        Guid CreateUser();
    }
}