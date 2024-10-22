using System.Collections.Generic;
using System.Threading.Tasks;
using DesnaInfo.Domain.Entities;

namespace DesnaInfo.DataAccess
{
    public interface IUserDataController
    {
        Task<List<User>> GetAll();
        Task<int> AddUser(User user);
        Task<int> RemoveUser(string modelMessengerId);
    }
}