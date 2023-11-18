using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Models;
using CyberBook_API.Interfaces;
using CyberBook_API.Dal;
using CyberBook_API.Dal.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CyberBook_API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CheckAndTestController : ControllerBase
    {
        private readonly IAccountsRepository _accountRepository = new AccountsRepository();
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();

        [HttpGet("GetAllAccounts")]
        [Authorize]
        public async Task<ServiceResponse<IEnumerable<Account>>> GetAllAccounts()
        {
            var output = new ServiceResponse<IEnumerable<Account>>();
            output.Data = await _accountRepository.GetAll();
            output.Success = true;
            output.Message = "Get OK";
            return output;
        }

        [HttpGet("ListCyber")]
        [Authorize]
        public async Task<ServiceResponse<IEnumerable<Cyber>>> ListCyber()
        {
            var output = new ServiceResponse<IEnumerable<Cyber>>();
            var outt = await _cybersRepository.GetAll();

            output.Data = outt;
            output.Success = true;
            output.Message = "Get OK";
            return output;
        }

        [HttpGet("UpdateCyber/{id}")]
        public async Task<ServiceResponse<Cyber>> UpdateCyber(int id)
        {
            var output = new ServiceResponse<Cyber>();
            //var cyber = new Cyber();
            var cyber = (await _cybersRepository.FindBy(x => x.Id == id)).FirstOrDefault();
            if (cyber == null)
            {
                output.Data = null;
                output.Success = false;
                output.Message = "Not Found Cyber has ID = " + id;
            }
            else
            {
                cyber.Id = 2;
                cyber.CyberName = "Cyber C UPDATED";
                cyber.Address = "Thường Tín, Hà Nội, Việt Nam";
                cyber.PhoneNumber = "0988888888";
                cyber.CyberDescription = "Description cyber C UPDATED Checker";
                cyber.RatingPoint = 4;
                cyber.BossCyberName = "Nguyễn Đình Huynh";
                cyber.lat = "12.21111";
                cyber.lng = "11.22222";

                var statusUpdate = await _cybersRepository.Update(cyber, cyber.Id);
                if (statusUpdate == -1)
                {
                    output.Data = null;
                    output.Success = false;
                    output.Message = "Update Fail";
                }
                await _cybersRepository.Save();
                output.Data = cyber;
                output.Success = true;
                output.Message = "Update Successfull";

            }
            return output;
        }

        [HttpGet("CreateCyber/{id}")]
        public async Task<ServiceResponse<Cyber>> CreateCyber(int id)
        {
            var output = new ServiceResponse<Cyber>();
            //var cyber = new Cyber();
            var cyber = (await _cybersRepository.FindBy(x => x.Id == id)).FirstOrDefault();
            if (cyber == null)
            {
                var c = new Cyber();
                c.CyberName = "Cyber D UPDATED";
                c.Address = "Hà Đông, Hà Nội, Việt Nam";
                c.PhoneNumber = "0988888888";
                c.CyberDescription = "Description cyber D Insert";
                c.RatingPoint = 4;
                c.BossCyberName = "Nguyễn Đình Huynh";
                c.lat = "12.21111";
                c.lng = "11.22222";

                await _cybersRepository.Create(c);
                await _cybersRepository.Save();
                output.Data = c;
                output.Success = true;
                output.Message = "Create Successfull";
            }
            else
            {
                output.Data = null;
                output.Success = false;
                output.Message = "Existing Cyber has ID = " + id + ". You canot create new Cyber";

            }
            return output;
        }

        [HttpPost("GetUserLogin")]
        public async Task<ServiceResponse<User>> GetUserLogin([FromBody] Account acc)
        {
            var output = new ServiceResponse<User>();
            var account = (await _accountRepository.FindBy(x => x.Username.Equals(acc.Username) && x.Password.Equals(acc.Password))).FirstOrDefault();
            if (account != null)
            {
                var user = (await _usersRepository.FindBy(x => x.Id == account.Id)).FirstOrDefault();
                if (user != null)
                {
                    output.Data = user;
                    output.Success = true;
                    output.Message = "Account EXISTING";
                    return output;
                }
                output.Data = null;
                output.Success = false;
                output.Message = "Account EXISTING";
                return output;
            }
            output.Data = null;
            output.Success = false;
            output.Message = "Username or Password not correct";
            return output;
        }

        [HttpPost("GetUserLoginByUsername")]
        public async Task<ServiceResponse<User>> GetUserLoginByUsernameAndPassword()
        {
            var output = new ServiceResponse<User>();

            string username = HttpContext.User.Identity.Name;

            var account = (await _accountRepository.FindBy(x => x.Username.Equals(username))).FirstOrDefault();
            if (account != null)
            {
                var user = (await _usersRepository.FindBy(x => x.Id == account.Id)).FirstOrDefault();
                if (user != null)
                {
                    output.Data = user;
                    output.Success = true;
                    output.Message = "Account EXISTING";
                    return output;
                }
                output.Data = null;
                output.Success = false;
                output.Message = "Account EXISTING";
                return output;
            }
            output.Data = null;
            output.Success = false;
            output.Message = "Username or Password not correct";
            return output;
        }


    }
}
