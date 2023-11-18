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
    [Authorize]
    [ApiController]
    public class CyberRateUserController : ControllerBase
    {
        private readonly ICyberRateUserRepository _cyberRateUserRepository = new CyberRateUserRepository();
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();
        private readonly IAccountsRepository _accountsRepository = new AccountsRepository();

        /// <summary>
        ///  thêm một bản ghi mới cho Cyber đánh giá một User
        ///  huynhnd53
        /// </summary>
        /// <param name="ratingUser"></param>
        /// <returns></returns>
        [HttpPost("AddNewRateUser")]
        public async Task<ActionResult> AddNewRateUser([FromBody] RatingUser ratingUser)
        {
            var output = new ServiceResponse<RatingUser>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var cyber = await _cybersRepository.GetCyberById(ratingUser.CyberId);
                    if (cyber != null)
                    {
                        var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                        var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                        //check user hiện tại có quyền trong Cyber này không?
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                        {
                            if (ratingUser.CyberId != null && ratingUser.UsersId != null)
                            {
                                //user được rate có tồn tại không?
                                var user = await _usersRepository.GetUserByUserID(ratingUser.UsersId);
                                if (cyber != null && user != null)
                                {
                                    var rating = await _cyberRateUserRepository.RateUserExist(ratingUser.CyberId, ratingUser.UsersId);
                                    if (rating == null)
                                    {
                                        var rcRateUser = new RatingUser
                                        {
                                            CyberId = ratingUser.CyberId,
                                            RatePoint = ratingUser.RatePoint,
                                            CommentContent = ratingUser.CommentContent,
                                            UsersId = ratingUser.UsersId,
                                            CreatedDate = DateTime.Now,
                                            UpdatedDate = DateTime.Now,
                                            Edited = false
                                        };
                                        var newRating = await _cyberRateUserRepository.Create(rcRateUser);
                                        if (newRating != null)
                                        {
                                            output.Data = newRating;
                                            output.Success = true;
                                            output.Message = "Create new Rating Successfull";
                                            return Ok(output);
                                        }
                                        output.Message = "Create new rating Failed";
                                        return Ok(output);
                                    }
                                    output.Message = "Rating has existing";
                                    return Ok(output);
                                }
                                output.Message = "Cyber or User not exist";
                                return Ok(output);
                            }
                            output.Message = "CyberId || UsersId not allow Null ";
                            return Ok(output);
                        }
                        output.Message = "You NOT Permission this Function";
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
                output.Message = "Rating User Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// Update bản ghi cyber đánh giá User
        /// huynhnd53
        /// </summary>
        /// <param name="ratingUser"></param>
        /// <returns></returns>
        [HttpPost("UpdateRateUser")]
        public async Task<ActionResult> UpdateRateUser([FromBody] RatingUser ratingUser)
        {
            var output = new ServiceResponse<RatingUser>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                if (currentUser != null)
                {
                    var cyber = await _cybersRepository.GetCyberById(ratingUser.CyberId);
                    if (cyber != null)
                    {
                        //check user hiện tại có quyền trong Cyber này không?
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                        {
                            var user = await _usersRepository.GetUserByUserID(ratingUser.UsersId);
                            if (cyber != null && user != null)
                            {
                                var rating = await _cyberRateUserRepository.RateUserExist(ratingUser.CyberId, ratingUser.UsersId);
                                if (rating == null)
                                {
                                    rating.RatePoint = ratingUser.RatePoint;
                                    rating.CommentContent = ratingUser.CommentContent;
                                    rating.UpdatedDate = DateTime.Now;
                                    rating.Edited = true;
                                    var rs = await _cyberRateUserRepository.Update(rating, rating.Id);
                                    if (rs != -1)
                                    {
                                        output.Data = rating;
                                        output.Success = true;
                                        output.Message = "Update Successfull";
                                        return Ok(output);
                                    }
                                    output.Message = "Update Failed";
                                    return Ok(output);
                                }
                                output.Message = "Rating not exist";
                                return Ok(output);
                            }
                            output.Message = "User or Cyber not exist";
                            return Ok(output);
                        }
                        output.Message = "You NOT Permission this Function";
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
                output.Message = "UpdateRating Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// lấy bản ghi cyber đánh giá user bằng id của bản ghi
        /// huynhnd53
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetRateById")]
        public async Task<ActionResult> GetRateById(int id)
        {
            var output = new ServiceResponse<RatingUser>();
            output.Data = null;
            output.Success = false;
            try
            {
                var rating = await _cyberRateUserRepository.GetRateUserById(id);
                if (rating != null)
                {
                    output.Data = rating;
                    output.Success = true;
                    output.Message = "Successfull";
                }
                output.Message = "Rating not exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "UpdateRating Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// Xóa một bản đánh giá người dùng bằng Id
        /// huynhnd53
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("RemoveRateById")]
        public async Task<ActionResult> RemoveRateById(int id)
        {
            var output = new ServiceResponse<int>();
            output.Data = 0;
            try
            {
                var rating = await _cyberRateUserRepository.GetRateUserById(id);
                if (rating != null)
                {
                    if ((await _cyberRateUserRepository.Delete(rating)) != -1)
                    {
                        output.Message = "Successful";
                        output.Data = 1;
                        output.Success = true;
                        return Ok(output);
                    }
                    output.Message = "CAN NOT Delete RateUser record";
                    return Ok(output);
                }
                output.Message = "RateUser record not Exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "Remove RateUser record exception";
            }
            return Ok(output);
        }
    }
}
