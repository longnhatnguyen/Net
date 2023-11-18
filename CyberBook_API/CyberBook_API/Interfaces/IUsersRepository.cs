using CyberBook_API.Dal;
using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<string> getHelloAsync();
        Task<User> GetUserByAccountID(int accountId);
        Task<User> GetUserByUserID(int? userId);
        Task<bool> CheckComfirmPass(int userId, string code);
        Task<User> GetUserByEmailAndPhone(string email, string phone);
    }
}
