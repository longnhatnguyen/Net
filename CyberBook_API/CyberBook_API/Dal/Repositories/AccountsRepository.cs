using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Dal;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberBook_API.Dal.Repositories
{
    public class AccountsRepository : GenericRepository<Account>, IAccountsRepository
    {
        public async Task<Account> CheckLogin(string username, string password)
        {
            return (await FindBy(x => x.Username.Equals(username) && x.Password.Equals(password))).FirstOrDefault();
        }

        public bool CheckNull(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }

        public async Task<Account> GetAccountByID(int accountId)
        {
            return (await FindBy(x => x.Id == accountId)).FirstOrDefault();
        }

        public async Task<Account> GetAccountByUsername(string username)
        {
            return (await FindBy(x => x.Username.Equals(username))).FirstOrDefault();
        }
    }
}
