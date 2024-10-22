using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesnaInfo.DataAccess;
using DesnaInfo.Domain.Entities;
using DesnaInfo.ViewModels;

namespace DesnaInfo.Business.UserService
{
    class UserService : IUserService
    {
        private IUserDataController _userDataController;

        public UserService(IUserDataController userDataController)
        {
            _userDataController = userDataController;
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            var users = await _userDataController.GetAll();
            var materializedUsers = users.ToList();
            if (materializedUsers.Any())
            {
                var userViewModels = materializedUsers.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    MessengerId = u.MessengerId,
                    CreatedAt = u.CreatedAt
                });
                return userViewModels.ToList();
            }
            return null;
        }

        public Task<int> AddUser(UserViewModel user)
        {
            User dbUser = new User
            {
                Id = user.Id,
                MessengerId = user.MessengerId,
                CreatedAt = DateTime.Now
            };
            return _userDataController.AddUser(dbUser);
        }

        public Task<int> RemoveUser(string userId)
        {
            return _userDataController.RemoveUser(userId);
        }
    }
}