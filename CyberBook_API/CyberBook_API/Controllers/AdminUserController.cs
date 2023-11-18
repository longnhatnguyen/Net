using CyberBook_API.Dal.Repositories;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository = new UsersRepository();

        [HttpGet("ListCyber")]
        //[Authorize]
        public async Task<ServiceResponse<IEnumerable<User>>> ListUser()
        {
            var output = new ServiceResponse<IEnumerable<User>>();

            var outt = await _usersRepository.GetAll();
            output.Data = outt;
            output.Success = true;
            output.Message = "Success";
            return output;
        }

        [HttpGet("ViewDetailUser")]
        public async Task<ActionResult> ViewDetailUser(int id)
        {
            var output = new ServiceResponse<User>();

            try
            {
                var detailUser = (await _usersRepository.FindBy(x => x.Id == id)).FirstOrDefault();
                if (detailUser != null)
                {
                    output.Data = detailUser;
                    output.Message = "Success";
                    output.Success = true;
                    return Ok(output);
                }
                else
                {
                    output.Message = "can not find";
                    return Ok(output);
                }
            }
            catch (Exception ex)
            {
                output.Message = ex.Message;
            }

            return Ok(output);
        }

        [HttpGet("SearchUserByName")]
        public async Task<ActionResult> SearchUserByName(string name)
        {
            var output = new ServiceResponse<IEnumerable<User>>();
            try
            {
                var users = (await _usersRepository.FindBy(x => x.Fullname.Contains(name))).ToList();
                if (users.Count() != 0)
                {
                    output.Success = true;
                    output.Message = "Success";
                    output.Data = users;
                }
                output.Message = "No user";
                output.Success = true;
            }
            catch (Exception ex)
            {
                output.Message = ex.Message;
            }
            return Ok(output);
        }


    }
}
