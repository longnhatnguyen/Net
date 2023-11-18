using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Interfaces;


namespace CyberBook_API.Dal.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public async Task<bool> CheckComfirmPass(int userId, string code)
        {
            var u = (await FindBy(x => x.Id == userId)).FirstOrDefault();
            if (u != null)
            {
                if (u.ComfirmPassword == code)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<string> getHelloAsync()
        {
            var u = (await FindBy(x => x.Id == 1)).FirstOrDefault();
            var s = u.Fullname;
            return s.ToString();
        }

        public async Task<User> GetUserByAccountID(int accountId)
        {
            return (await FindBy(x => x.AccountID == accountId)).FirstOrDefault();
        }

        public async Task<User> GetUserByEmailAndPhone(string email, string phone)
        {
            return (await FindBy(x => x.Email.Equals(email) && x.PhoneNumber.Equals(phone))).FirstOrDefault();
        }

        public async Task<User> GetUserByUserID(int? userId)
        {
            return (await FindBy(x => x.Id == userId)).FirstOrDefault();
        }
    }
}
