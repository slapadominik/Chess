using System;
using System.Collections.Generic;
using System.Linq;
using Chess.API.Entity;
using Chess.API.Exceptions;
using Chess.API.Persistence.DAO;
using Chess.API.Persistence.Interfaces;
using Chess.API.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Chess.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        
        public User GetUserById(Guid id)
        {
            var userDAO = _userRepository.GetUserById(id);
            if (userDAO == null)
            {
                throw new UserNotFoundException($"User with Id: [{id}] not found.");
            }

            return new User(userDAO.Id, userDAO.Username);
        }

        public IEnumerable<User> GetUsers()
        {
            var usersDao = _userRepository.GetUsers();
            return usersDao.Select(x => new User {Id = x.Id, Username = x.Username});
        }

        public Guid CreateUser()
        {
            User user = new User();
            _userRepository.AddUser(new UserDAO{Id = user.Id, Username = user.Username});

            _logger.LogInformation($"{user.Id} {user.Username} created.");
            return user.Id;
        }
    }
}