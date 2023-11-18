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
    public class SetUpHardwareController : ControllerBase
    {
        private readonly IHardwareConfigRepository _hardwareConfigRepository = new HardwareConfigRepository();
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();
        private readonly IAccountsRepository _accountsRepository = new AccountsRepository();


        /// <summary>
        /// tạo mới một cấu hình của máy 
        /// huynhnd53 30/10/2021
        /// </summary>
        /// <param name="hardwareIN"></param>
        /// <returns></returns>
        [HttpPost("CreateSlotHardwareConfig")]
        public async Task<ActionResult> CreateSlotHardwareConfig([FromBody] SlotHardwareConfig hardwareIN)
        {
            var output = new ServiceResponse<SlotHardwareConfig>();
            try
            {
                var usernameCrr = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                if (currentUser != null)
                {
                    var cyber = await _cybersRepository.GetCyberByBossCyberId(currentUser.Id);
                    if (cyber != null)
                    {
                        //check user hiện tại có quyền trong Cyber này không?
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                        {
                            if (!hardwareIN.NameHardware.Equals(""))
                            {
                                var hardwwareCheck = await _hardwareConfigRepository.GetSlotHardwareByNameAndCyber(hardwareIN.NameHardware, cyber.Id);
                                if (hardwwareCheck == null)
                                {
                                    var hardwareTemp = new SlotHardwareConfig
                                    {
                                        Gpu = hardwareIN.Gpu,
                                        CyberID = cyber.Id,
                                        Cpu = hardwareIN.Cpu,
                                        Monitor = hardwareIN.Monitor,
                                        NameHardware = hardwareIN.NameHardware,
                                        Ram = hardwareIN.Ram,
                                        CreatedDate = DateTime.Now,
                                        UpdateDate = DateTime.Now
                                    };
                                    var newHardware = await _hardwareConfigRepository.Create(hardwareTemp);
                                    if (newHardware != null)
                                    {
                                        output.Data = newHardware;
                                        output.Message = "Successful";
                                        output.Success = true;
                                        return Ok(output);
                                    }
                                    output.Message = "CAN NOT Create new Hardwares";
                                    return Ok(output);
                                }
                                output.Message = "Hardware has exist";
                                return Ok(output);
                            }
                            output.Message = "Name of Hardware NOT allow empty";
                            return Ok(output);
                        }
                        output.Message = "You dont have Permission for this Function";
                        return Ok(output);
                    }
                    output.Message = "Cyber NOT exist";
                    return Ok(output);
                }
                output.Message = "You NOT Login";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "Create NEW slot has exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// sửa thông tin của một cấu hình 
        /// huynhnd53  30/10/2021
        /// </summary>
        /// <param name="hardwareIN"></param>
        /// <returns></returns>
        [HttpPost("UpdateSlotHardwareConfig")]
        public async Task<ActionResult> UpdateSlotHardwareConfig([FromBody] SlotHardwareConfig hardwareIN)
        {
            var output = new ServiceResponse<SlotHardwareConfig>();
            try
            {
                var usernameCrr = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                if (currentUser != null)
                {
                    var cyber = await _cybersRepository.GetCyberByBossCyberId(currentUser.Id);
                    if (cyber != null)
                    {
                        //check user hiện tại có quyền trong Cyber này không?
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                        {
                            var hardware = await _hardwareConfigRepository.GetSlotHardwareById(hardwareIN.Id);
                            if (hardware != null)
                            {
                                hardware.Monitor = hardwareIN.Monitor;
                                hardware.Gpu = hardwareIN.Gpu;
                                hardware.Cpu = hardwareIN.Cpu;
                                hardware.Ram = hardwareIN.Ram;
                                hardware.NameHardware = hardwareIN.NameHardware;
                                hardware.UpdateDate = DateTime.Now;
                                if ((await _hardwareConfigRepository.Update(hardware, hardware.Id)) != -1)
                                {
                                    output.Data = hardware;
                                    output.Message = "Successful";
                                    output.Success = true;
                                    return Ok(output);
                                }
                                output.Message = "Update Fail";
                                return Ok(output);
                            }
                            output.Message = "Hardware NOT exist";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission for this Function";
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
                output.Message = "Update Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// Xóa một cấu hình máy 
        /// huynhnd53 30/10/2021
        /// </summary>
        /// <param name="hardwareId"></param>
        /// <returns></returns>
        [HttpDelete("RemoveSlotHardwareConfig")]
        public async Task<ActionResult> RemoveSlotHardwareConfig(int hardwareId)
        {
            var output = new ServiceResponse<int>();
            try
            {
                var usernameCrr = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                if (currentUser != null)
                {
                    var cyber = await _cybersRepository.GetCyberByBossCyberId(currentUser.Id);
                    if (cyber != null)
                    {
                        //check user hiện tại có quyền trong Cyber này không?
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                        {
                            var hardware = (await _hardwareConfigRepository.FindBy(x => x.Id == hardwareId)).FirstOrDefault();
                            if (hardware != null)
                            {
                                if ((await _hardwareConfigRepository.Delete(hardware)) != -1)
                                {
                                    output.Message = "Successful";
                                    output.Data = 1;
                                    output.Success = true;
                                    return Ok(output);
                                }
                                output.Message = "CAN NOT Remove SlotHardwareConfig";
                                return Ok(output);
                            }
                            output.Message = "HardWare NOT exist";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission for this Function";
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
                output.Message = "Remove SlotHardwareConfig Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// lấy danh sách các cấu hình máy của cyber
        /// huynhnd53 30/10/2021
        /// </summary>
        /// <param name="cyberId"></param>
        /// <returns></returns>
        [HttpGet("GetHardwaresByCyberId")]
        public async Task<ActionResult> GetHardwaresByCyberId(int cyberId)
        {
            var output = new ServiceResponse<IEnumerable<SlotHardwareConfig>>();
            try
            {
                if ((await _cybersRepository.FindBy(x => x.Id == cyberId)) != null)
                {
                    var lstHardware = await _hardwareConfigRepository.GetSlotHardwareByCyberId(cyberId);
                    if (lstHardware.Any())
                    {
                        output.Data = lstHardware;
                        output.Message = "Successful";
                        output.Success = true;
                        return Ok(output);
                    }
                    output.Message = "dont any hardware";
                    return Ok(output);
                }
                output.Message = "Cyber NOT Exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "GetHardwaresByCyberId Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// lấy thông tin của một cấu hình 
        /// huynhnd53 30/10/2021
        /// </summary>
        /// <param name="hardwareId"></param>
        /// <returns></returns>
        [HttpGet("GetHardwaresById")]
        public async Task<ActionResult> GetHardwaresById(int hardwareId)
        {
            var output = new ServiceResponse<SlotHardwareConfig>();
            try
            {
                var hardware = await _hardwareConfigRepository.GetSlotHardwareById(hardwareId);
                if (hardware != null)
                {
                    output.Success = true;
                    output.Message = "Success";
                    output.Data = hardware;
                }
                output.Message = "hardware not exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "GetHardwaresById Exception";
            }
            return Ok(output);
        }

    }
}
