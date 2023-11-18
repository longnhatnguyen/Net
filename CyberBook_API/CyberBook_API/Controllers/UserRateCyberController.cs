using CyberBook_API.Dal.Repositories;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.AccountView;
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
    public class UserRateCyberController : ControllerBase
    {
        private readonly IUserRateCyberRepository _userRateCyberRepository = new UserRateCyberRepository();
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();
        private readonly IAccountsRepository _accountsRepository = new AccountsRepository();
        private readonly ICyberAccountRepository _cyberAccountRepository = new CyberAccountRepository();
        private readonly IOrderRepository _orderRepository = new OrderRepository();


        /// <summary>
        ///  User đánh giá một Cyber
        ///  huynhnd53
        /// </summary>
        /// <param name="rateCyber"></param>
        /// <returns></returns>
        [HttpPost("AddUserRateCyber")]
        public async Task<ActionResult> AddUserRateCyber([FromBody] RatingCyber rateCyber)
        {
            var output = new ServiceResponse<RatingCyber>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                    var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                    var cyber = await _cybersRepository.GetCyberById(rateCyber.CyberId);
                    if (cyber != null)
                    {
                        var cyberAccount = await _cyberAccountRepository.GetCyberAccountDetail(cyber.Id, currentUser.Id);
                        if (cyberAccount != null)
                        {
                            var order = await _orderRepository.GetOrderById(rateCyber.OrderId);
                            if (order != null)
                            {
                                var rateCyberCheck = await _userRateCyberRepository.GetRateCyberExist(currentUser.Id, cyber.Id, order.Id);
                                if (rateCyberCheck != null)
                                {
                                    var rateTemp = new RatingCyber
                                    {
                                        UserId = currentUser.Id,
                                        RatePoint = rateCyber.RatePoint,
                                        CommentContent = rateCyber.CommentContent,
                                        CyberId = rateCyber.CyberId,
                                        CreatedDate = DateTime.Now,
                                        UpdatedDate = DateTime.Now,
                                        Edited = false,
                                        OrderId = rateCyber.OrderId
                                    };
                                    var newRate = await _userRateCyberRepository.Create(rateTemp);
                                    if (newRate != null)
                                    {
                                        var lstRateCyber = await _userRateCyberRepository.GetListRateCyberByCyberId(cyber.Id);
                                        double sum = 0;
                                        int count = 0;
                                        foreach (var x in lstRateCyber)
                                        {
                                            sum += x.RatePoint;
                                            count++;
                                        }
                                        cyber.RatingPoint = sum / count;
                                        await _cybersRepository.Update(cyber, cyber.Id);

                                        output.Message = "Successfull";
                                        output.Data = newRate;
                                        output.Success = true;
                                        return Ok(output);
                                    }
                                    output.Message = "CAN NOT Create RateCyber";
                                    return Ok(output);
                                }
                                output.Message = "Rate record has exist";
                                return Ok(output);
                            }
                            output.Message = "Order NOT exist";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission this Function";
                        return Ok(output);
                    }
                    output.Message = "Cyber NOT exist";
                    return Ok(output);
                }
                output.Message = "You NOT LOGIN";
                return Ok(output);
            }
            catch (Exception e)
            {
                output.Message = e.Message;
            }
            return Ok(output);
        }

        /// <summary>
        /// Update bản ghi User đánh giá Cyber
        /// huynhnd53
        /// </summary>
        /// <param name="ratingUser"></param>
        /// <returns></returns>
        [HttpPost("UpdateRateCyber")]
        public async Task<ActionResult> UpdateRateCyber([FromBody] RatingCyber rateCyber)
        {
            var output = new ServiceResponse<RatingCyber>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                    var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                    var cyber = await _cybersRepository.GetCyberById(rateCyber.CyberId);
                    if (cyber != null)
                    {
                        var cyberAccount = await _cyberAccountRepository.GetCyberAccountDetail(cyber.Id,currentUser.Id);
                        if (cyberAccount != null)
                        {
                            var order = await _orderRepository.GetOrderById(rateCyber.OrderId);
                            if (order != null)
                            {
                                var rateCyberCheck = await _userRateCyberRepository.GetRateCyberExist(currentUser.Id, cyber.Id, order.Id);
                                if (rateCyberCheck != null)
                                {
                                    if ((await _userRateCyberRepository.Update(rateCyberCheck, rateCyberCheck.Id)) != -1)
                                    {
                                        var lstRateCyber = await _userRateCyberRepository.GetListRateCyberByCyberId(cyber.Id);
                                        double sum = 0;
                                        int count = 0;
                                        foreach (var x in lstRateCyber)
                                        {
                                            sum += x.RatePoint;
                                            count++;
                                        }
                                        cyber.RatingPoint = sum / count;
                                        await _cybersRepository.Update(cyber, cyber.Id);

                                        output.Message = "Successfull";
                                        output.Data = rateCyberCheck;
                                        output.Success = true;
                                        return Ok(output);
                                    }
                                    output.Message = "Update Rate Cyber FAIL";
                                    return Ok(output);
                                }
                                output.Message = "Rate NOT exist";
                                return Ok(output);
                            }
                            output.Message = "Order NOT exist";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission this Function";
                        return Ok(output);
                    }
                    output.Message = "Cyber NOT exist";
                    return Ok(output);
                }
                output.Message = "You NOT LOGIN";
                return Ok(output);
            }
            catch (Exception e)
            {
                output.Message = e.Message;
            }
            return Ok(output);
        }

        /// <summary>
        /// Xóa một bản người dùng đánh giá Cyber
        /// huynhnd53
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("RemoveRateById")]
        public async Task<ActionResult> RemoveRateById(int id)
        {
            var output = new ServiceResponse<string>();
            //check user hiện tại
            try
            {
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var accountCrr = (await _accountsRepository.FindBy(x => x.Username.Equals(usernameCrr))).FirstOrDefault();
                    var currentUser = (await _usersRepository.FindBy(x => x.AccountID == accountCrr.Id)).FirstOrDefault();
                    var rateRecord = (await _userRateCyberRepository.FindBy(x => x.Id == id && x.UserId == currentUser.Id)).FirstOrDefault();
                    if (rateRecord != null)
                    {
                        var cyber = (await _cybersRepository.FindBy(x => x.Id == rateRecord.CyberId)).FirstOrDefault();
                        if (cyber != null)
                        {
                            if ((await _userRateCyberRepository.Delete(rateRecord)) != -1)
                            {
                                var lstRateCyber = await _userRateCyberRepository.GetListRateCyberByCyberId(cyber.Id);
                                double sum = 0;
                                int count = 0;
                                foreach (var x in lstRateCyber)
                                {
                                    sum += x.RatePoint;
                                    count++;
                                }
                                cyber.RatingPoint = sum / count;
                                await _cybersRepository.Update(cyber, cyber.Id);

                                output.Message = "Delete Rate Record Successfull";
                                output.Data = "Xóa thành công bản ghi đánh giá Cyber " + cyber.CyberName;
                                output.Success = true;
                                return Ok(output);
                            }
                            output.Message = "CAN NOT remove this Rate Cyber";
                            return Ok(output);
                        }
                        output.Message = "Cyber NOT exist";
                        return Ok(output);
                    }
                    output.Message = "No Record avaiable";
                    return Ok(output);
                }
                output.Message = "You NOT LOGIN";
                return Ok(output);
            }
            catch (Exception e)
            {
                output.Message = e.Message;
            }
            return Ok(output);
        }

        [HttpGet("GetRateCyberById")]
        public async Task<ActionResult> GetRateCyberById(int id)
        {
            var output = new ServiceResponse<RatingCyber>();
            try
            {
                var rateRecord = await _userRateCyberRepository.GetRateCyberById(id);
                if (rateRecord != null)
                {
                    output.Message = "get RateCyber Record Successfull";
                    output.Data = rateRecord;
                    output.Success = true;
                    return Ok(output);
                }
                output.Message = "No Record avaiable";
                return Ok(output);
            }
            catch (Exception e)
            {
                output.Message = e.Message;       
            }
            return Ok(output);
        }
    }
}
