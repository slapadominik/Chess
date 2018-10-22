using System;
using Chess.API.Entity;

namespace Chess.API.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(Guid id);
    }
}