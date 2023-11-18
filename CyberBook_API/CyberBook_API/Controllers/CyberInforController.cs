using CyberBook_API.Dal.Repositories;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CyberBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CyberInforController : Controller
    {
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        private readonly ICyberRateUserRepository _cyberRateUserRepository = new CyberRateUserRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();
        private readonly IAccountsRepository _accountsRepository = new AccountsRepository();

        //[HttpPost("EditCyberInfor")]
        //public async Task<string> EditCyberInfor([FromBody] Cyber cyber)
        //{
        //    try
        //    {
        //        //var cyberInfor = (await _cybersRepository.FindBy(x => x.Id == cyber.Id)).FirstOrDefault();
        //        var cyberInfor = await _cybersRepository.GetCyberById(cyber.Id);
        //        if (cyberInfor != null)
        //        {
        //            cyberInfor.CyberName = cyber.CyberName;
        //            cyberInfor.Address = cyber.Address;
        //            cyberInfor.PhoneNumber = cyber.PhoneNumber;
        //            cyberInfor.CyberDescription = cyber.CyberDescription;
        //            cyberInfor.RatingPoint = cyber.RatingPoint;
        //            cyberInfor.BossCyberName = cyber.BossCyberName;
        //            cyberInfor.BossCyberID = cyber.BossCyberID;
        //            cyberInfor.lat = cyber.lat;
        //            cyberInfor.lng = cyber.lng;

        //            var rs = await _cybersRepository.Update(cyberInfor, cyberInfor.Id);

        //            if (rs != -1)
        //            {
        //                return "Update Successfull";
        //            }
        //            return "Update Failed";
        //        }
        //        return "User or Cyber not exist";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}


        [HttpPost("EditCyberInfor")]
        public async Task<ActionResult> EditCyberInfor([FromBody]Cyber cyberIn)
        {
            var output = new ServiceResponse<Cyber>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var cyber = await _cybersRepository.GetCyberById(cyberIn.Id);
                    if (cyber != null)
                    {
                        var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                        var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                        //check user hiện tại có quyền trong Cyber này không?
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                        {
                            cyber.CyberName = cyberIn.CyberName;
                            cyber.Address = cyberIn.Address;
                            cyber.PhoneNumber = cyberIn.PhoneNumber;
                            cyber.CyberDescription = cyberIn.CyberDescription;
                            cyber.RatingPoint = cyberIn.RatingPoint;
                            cyber.BossCyberName = cyberIn.BossCyberName;
                            cyber.BossCyberID = cyberIn.BossCyberID;
                            cyber.lat = cyberIn.lat;
                            cyber.lng = cyberIn.lng;
                            var rs = await _cybersRepository.Update(cyber, cyber.Id);
                            if (rs != -1)
                            {
                                output.Data = cyber;
                                output.Message = "Update EditCyberInfor OK";
                                output.Success = true;
                                return Ok(output);
                            }
                            output.Message = "Update EditCyberInfor FAIL";
                            return Ok(output);
                        }
                        output.Message = "You NOT cyber Admin";
                        return Ok(output);
                    }
                    output.Message = "Cyber NOT Exist";
                    return Ok(output);
                }
                output.Message = "You NOT LOGIN";
                return Ok(output);
            }
            catch (Exception ex)
            {
                output.Message = "Exception "+ex.Message;
            }
            return Ok(output);
        }
    }
}
