using CyberBook_API.Dal.Repositories;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.AccountView;
using CyberBook_API.ViewModel.PagingView;
using CyberBook_API.ViewModel.SlotViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CyberBook_API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ISlotsRepository _slotsRepository = new SlotsRepository();
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        private readonly IAccountsRepository _accountsRepository = new AccountsRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();
        private readonly IRoomRepository _roomRepository = new RoomRepository();
        private readonly IHardwareConfigRepository _hardwareConfigRepository = new HardwareConfigRepository();
        private readonly IStatusSlotRepository _statusSlotRepository = new StatusSlotRepository();
        private readonly ICyberAccountRepository _cyberAccountRepository = new CyberAccountRepository();


        /// <summary>
        /// Người dùng thêm mới 1 Cyber Account 
        ///  huynhnd53 02/11/2021
        /// </summary>
        /// <param name="cyberAccount"></param>
        /// <returns></returns>
        [HttpPost("AddNewCyberAccount")]
        public async Task<ActionResult> AddNewCyberAccount([FromBody] CyberAccount cyberAccount)
        {
            var output = new ServiceResponse<CyberAccount>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                //check user hiện tại có quyền trong Cyber này không?
                if (currentUser != null)
                {
                    var cyber = await _cybersRepository.GetCyberById(cyberAccount.CyberId);
                    if (cyber != null)
                    {
                        if (!cyberAccount.Username.Equals("") && !cyberAccount.Password.Equals(""))
                        {
                            var cyberAccCheck = await _cyberAccountRepository.CyberAccountExist(currentUser.Id, cyber.Id, cyberAccount.Username);

                            if (cyberAccCheck == null)
                            {
                                var cyberAccountTemp = new CyberAccount
                                {
                                    UserId = currentUser.Id,
                                    CyberId = cyber.Id,
                                    PhoneNumber = cyberAccount.PhoneNumber,
                                    CyberName = cyber.CyberName,
                                    Username = cyberAccount.Username,
                                    Password = cyberAccount.Password
                                };
                                var newCyberAccount = await _cyberAccountRepository.Create(cyberAccountTemp);
                                if (newCyberAccount != null)
                                {
                                    output.Data = newCyberAccount;
                                    output.Message = "Successfull";
                                    output.Success = true;
                                    return Ok(output);
                                }
                                output.Message = "CAN NOT Create New CyberAccount";
                                return Ok(output);
                            }
                            output.Message = "this Account was exist";
                            return Ok(output);
                        }
                        output.Message = "username or password NOT allow Empty";
                        return Ok(output);
                    }
                    output.Message = "Cyber NOT exist";
                    return Ok(output);
                }
                output.Message = "User NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "AddNewCyberAccount Exception";
            }

            return Ok(output);
        }

        /// <summary>
        /// Người dùng sửa một bản ghi Cyber Account
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="cyberAccount"></param>
        /// <returns></returns>
        [HttpPost("EditCyberAccount")]
        public async Task<ActionResult> EditCyberAccount([FromBody] CyberAccount cyberAccount)
        {
            var output = new ServiceResponse<CyberAccount>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                //check user hiện tại có quyền trong Cyber này không?
                if (currentUser != null)
                {
                    var cyber = await _cybersRepository.GetCyberById(cyberAccount.CyberId);
                    if (cyber != null)
                    {
                        if (!cyberAccount.Username.Equals("") && !cyberAccount.Password.Equals(""))
                        {
                            var cyberAccCheck = await _cyberAccountRepository.CyberAccountExist(currentUser.Id,cyber.Id,cyberAccount.Username);

                            if (cyberAccCheck != null)
                            {
                                cyberAccCheck.CyberId = cyberAccount.CyberId;
                                cyberAccCheck.CyberName = cyberAccount.CyberName;
                                cyberAccCheck.Username = cyberAccount.Username;
                                cyberAccCheck.Password = cyberAccount.Password;

                                var rs = await _cyberAccountRepository.Update(cyberAccCheck, cyberAccCheck.ID);
                                if (rs != -1)
                                {
                                    output.Data = cyberAccCheck;
                                    output.Message = "Successfull";
                                    output.Success = true;
                                    return Ok(output);
                                }
                                output.Message = "CAN NOT Edit  CyberAccount";
                                return Ok(output);
                            }
                            output.Message = "CyberAccount NOT exist";
                            return Ok(output);
                        }
                        output.Message = "username or password NOT allow Empty";
                        return Ok(output);
                    }
                    output.Message = "Cyber NOT exist";
                    return Ok(output);
                }
                output.Message = "User NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "EditCyberAccount Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// Người dùng xóa đi CyberAccount
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="cyberAccount"></param>
        /// <returns></returns>
        [HttpDelete("RemoveCyberAccount")]
        public async Task<ActionResult> RemoveCyberAccount([FromBody] CyberAccount cyberAccount)
        {
            var output = new ServiceResponse<int>();
            try
            {
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                if (currentUser != null)
                {
                    var cyber = await _cybersRepository.GetCyberById(cyberAccount.CyberId);
                    if (cyber != null)
                    {
                        var cyberAccCheck = await _cyberAccountRepository.CyberAccountExist(currentUser.Id, cyber.Id, cyberAccount.Username);
                        if (cyberAccCheck != null)
                        {
                            if ((await _cyberAccountRepository.Delete(cyberAccCheck)) != -1)
                            {
                                output.Data = 1;
                                output.Message = "Successfull";
                                output.Success = true;
                                return Ok(output);
                            }
                            output.Message = "CAN NOT Remove CyberAccount";
                            return Ok(output);
                        }
                        output.Message = "CyberAccount NOT exist";
                        return Ok(output);
                    }
                    output.Message = "Cyber NOT exist";
                    return Ok(output);
                }
                output.Message = "You NOT LOGIN";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "RemoveCyberAccount Exception";
            }
            return Ok(output);
        }


        /// <summary>
        /// người dùng xem thông tin của CyberAccount của họ
        ///   huynhnd53 02/11/2021
        /// </summary>
        /// <param name="cyberAccountId"></param>
        /// <returns></returns>
        [HttpGet("GetDetailCyberAccount")]
        public async Task<ActionResult> GetDetailCyberAccount(int cyberAccountId)
        {
            var output = new ServiceResponse<CyberAccount>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                if (currentUser != null)
                {
                    //var cyberAccountCheck = await _cyberAccountRepository.FindBy(x => x.ID == cyberAccountId && x.UserId == currentUser.Id)).FirstOrDefault();
                    var cyberAccountCheck = await _cyberAccountRepository.GetCyberAccountDetail(cyberAccountId, currentUser.Id);
                    if (cyberAccountCheck != null)
                    {
                        output.Data = cyberAccountCheck;
                        output.Message = "Successfully";
                        output.Success = true;
                        return Ok(output);
                    }
                    output.Message = "CAN NOT find your CyberAccount or You dont have Permission for view this Account Cyber";
                    return Ok(output);
                }
                output.Message = "You NOT LOGIN";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "RemoveCyberAccount Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// lấy ra danh sách CyberAccount của người dùng hiện tại
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <returns></returns>
        [HttpGet("ViewListCyberAccount")]
        public async Task<ActionResult> ViewListCyberAccount([FromBody] PagingOutput<User> paging)
        {
            var output = new PagingOutput<IEnumerable<CyberAccount>>();
            try
            {
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                if (currentUser != null)
                {
                    var result = await _cyberAccountRepository.GetListCyberAccountByUserId(currentUser.Id, paging.Index, paging.PageSize);
                    if (result.Data.Any())
                    {
                        output = result;
                        output.Message = "Get your List CyberAccount OK";
                        return Ok(output);
                    }
                    output.Message = "dont have any cyber account";
                    return Ok(output);
                }
                output.Message = "You NOT Login";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "ViewListCyberAccount Exception";
            }
            return Ok(output);
        }

        [HttpPost("EditUserInfor")]
        public async Task<ActionResult> EditUserInfor()
        {
            return Ok("ok");
        }
    }
}
