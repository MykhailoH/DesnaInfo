using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesnaInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DesnaInfo.DataAccess
{
    class UserDataController : IUserDataController
    {
        private readonly IConfiguration _configuration;
        public UserDataController(IConfiguration configuration)
        {
            _configuration = configuration;
            using var db = new DesnaInfoDbContext(_configuration);
            db.Database.Migrate();
        }
        public Task<List<User>> GetAll()
        {
            using var context = new DesnaInfoDbContext(_configuration);
            var users = context.Users.ToList();
            return Task.FromResult(users);
        }

        public Task<int> AddUser(User user)
        {
            using var context = new DesnaInfoDbContext(_configuration);
            if (context.Users.Any(u => u.MessengerId == user.MessengerId))
            {
                return Task.FromResult(0);
            }
            context.Users.Add(user);
            return context.SaveChangesAsync();
        }

        public Task<int> RemoveUser(string modelMessengerId)
        {
            using var context = new DesnaInfoDbContext(_configuration);
            var userToRemove = context.Users.FirstOrDefault(u => u.MessengerId == modelMessengerId);
            if (userToRemove != null)
            {
                context.Users.Remove(userToRemove);
                return context.SaveChangesAsync();
            }

            return Task.FromResult(0);
        }
    }
}
