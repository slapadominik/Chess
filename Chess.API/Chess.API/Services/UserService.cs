using System;
using System.Collections.Generic;
using System.Linq;
using Chess.API.Entity;
using Chess.API.Persistence.DAO;
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
            if (userDAO == null)
            {
                return null;
            }
            return new User {Id = userDAO.Id, Username = userDAO.Username};
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
            return user.Id;
        }
    }
}