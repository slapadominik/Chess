using System;
using Chess.API.Entity;
using Chess.API.Persistence.Interfaces;
using Chess.API.Services.Interfaces;

namespace Chess.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public User GetUserById(Guid id)
        {
            var userDAO = _userRepository.GetUserById(id);
            return new User {Id = userDAO.Id, Username = userDAO.Username};
        }
    }
}