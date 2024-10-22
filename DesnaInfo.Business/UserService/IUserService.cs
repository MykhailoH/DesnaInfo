
using System.Collections.Generic;
using DesnaInfo.Domain.Entities;
using System.Threading.Tasks;
using DesnaInfo.ViewModels;

namespace DesnaInfo.Business.UserService
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAll();

        Task<int> AddUser(UserViewModel user);
        Task<int> RemoveUser(string userId);
    }
}