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
    public class SlotsController : ControllerBase
    {

        private readonly ISlotsRepository _slotsRepository = new SlotsRepository();
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        private readonly IAccountsRepository _accountsRepository = new AccountsRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();
        private readonly IRoomRepository _roomRepository = new RoomRepository();
        private readonly IHardwareConfigRepository _hardwareConfigRepository = new HardwareConfigRepository();
        private readonly IStatusSlotRepository _statusSlotRepository = new StatusSlotRepository();

        /// <summary>
        /// tạo mới một slot
        ///    huynhnd53 02/11/2021
        /// </summary>
        /// <param name="slotViewModelIn"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("CreateNewSlot")]
        public async Task<ActionResult> CreateNewSlot([FromBody] SlotViewModelIn slotViewModel)
        {
            var output = new ServiceResponse<Slot>();
            try
            {
                var cyber = await _cybersRepository.GetCyberById(slotViewModel.CyberId);
                if (cyber != null)
                {
                    //check user hiện tại
                    var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                    if (usernameCrr != null)
                    {
                        var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                        var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                        //check user hiện tại có quyền trong Cyber này không?
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                        {
                            var roomCheck = await _roomRepository.GetRoomById(slotViewModel.RoomId);
                            if (roomCheck != null)
                            {
                                var slotCheck = await _slotsRepository.GetSlotById(slotViewModel.Slot.Id);
                                if (slotCheck == null)
                                {
                                    //check xem cái thông tin phần cứng đấy có thuộc trong cyber đó không
                                    var hardwareConfig = await _hardwareConfigRepository.GetSlotHardwareByCyberId(slotViewModel.CyberId);
                                    if (hardwareConfig != null)
                                    {
                                        //check trạng thái của máy có tồn tại không?
                                        var statusSlot = await _statusSlotRepository.GetStatusSlotsById(slotViewModel.Slot.StatusId);
                                        if (statusSlot != null)
                                        {
                                            if (!slotViewModel.Slot.SlotName.Equals(""))
                                            {
                                                var slotTempt = new Slot
                                                {
                                                    RoomId = slotViewModel.RoomId,
                                                    SlotHardwareConfigId = slotViewModel.Slot.SlotHardwareConfigId,
                                                    SlotHardwareName = slotViewModel.Slot.SlotHardwareName,
                                                    StatusId = slotViewModel.Slot.StatusId,
                                                    SlotName = slotViewModel.Slot.SlotName,
                                                    SlotDescription = slotViewModel.Slot.SlotDescription,
                                                    SlotPositionX = slotViewModel.Slot.SlotPositionX,
                                                    SlotPositionY = slotViewModel.Slot.SlotPositionY
                                                };
                                                var newSlot = await _slotsRepository.Create(slotTempt);
                                                if (newSlot != null)
                                                {
                                                    output.Data = newSlot;
                                                    output.Message = "Create New Slot Successfully";
                                                    output.Success = true;
                                                    return Ok(output);
                                                }
                                                output.Message = "Create New Slot Fail";
                                                return Ok(output);

                                            }
                                            output.Message = "SlotName CAN NOT is Empty";
                                            return Ok(output);
                                        }
                                        output.Message = "statusSlot NOT exist";
                                        return Ok(output);
                                    }
                                    output.Message = "hardwareConfig NOT exist in this Cyber";
                                    return Ok(output);
                                }
                                output.Message = "Slot has exist";
                                return Ok(output);
                            }
                            output.Message = "Room NOT exist";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission for this Function";
                        return Ok(output);
                    }
                    output.Message = "You NOT Login";
                    return Ok(output);
                }
                output.Message = "Cyber NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "CreateNewSlot Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// sửa một slot 
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="slotViewModelIn"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("EditSlot")]
        public async Task<ActionResult> EditSlot([FromBody] SlotViewModelIn slotViewModelIn)
        {
            var output = new ServiceResponse<Slot>();
            try
            {
                var cyber = await _cybersRepository.GetCyberById(slotViewModelIn.CyberId);
                if (cyber != null)
                {
                    //check user hiện tại
                    var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                    if (usernameCrr != null)
                    {
                        var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                        var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                        //check user hiện tại có quyền trong Cyber này không?
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                        {
                            var roomCheck = await _roomRepository.GetRoomById(slotViewModelIn.RoomId);
                            if (roomCheck != null)
                            {
                                var slotCheck = await _slotsRepository.GetSlotById(slotViewModelIn.Slot.Id);
                                if (slotCheck != null)
                                {
                                    //check xem cái thông tin phần cứng đấy có thuộc trong cyber đó không
                                    var hardwareConfig = await _hardwareConfigRepository.GetSlotHardwareByCyberId(slotViewModelIn.CyberId);
                                    if (hardwareConfig != null)
                                    {
                                        //check trạng thái của máy có tồn tại không?
                                        var statusSlot = await _statusSlotRepository.GetStatusSlotsById(slotViewModelIn.Slot.StatusId);
                                        if (statusSlot != null)
                                        {
                                            if (!slotViewModelIn.Slot.SlotName.Equals(""))
                                            {
                                                slotCheck.SlotHardwareConfigId = slotViewModelIn.Slot.SlotHardwareConfigId;
                                                slotCheck.SlotHardwareName = slotViewModelIn.Slot.SlotHardwareName;
                                                slotCheck.StatusId = slotViewModelIn.Slot.StatusId;
                                                slotCheck.SlotName = slotViewModelIn.Slot.SlotName;
                                                slotCheck.SlotDescription = slotViewModelIn.Slot.SlotDescription;
                                                slotCheck.SlotPositionX = slotViewModelIn.Slot.SlotPositionX;
                                                slotCheck.SlotPositionY = slotViewModelIn.Slot.SlotPositionY;
                                                var newSlot = await _slotsRepository.Update(slotCheck, slotCheck.Id);
                                                if (newSlot != -1)
                                                {
                                                    output.Data = slotCheck;
                                                    output.Message = "Update Slot Successfully";
                                                    output.Success = true;
                                                    return Ok(output);
                                                }
                                                output.Message = "Update Slot Fail";
                                                return Ok(output);
                                            }
                                            output.Message = "SlotName CAN NOT is Empty";
                                            return Ok(output);
                                        }
                                        output.Message = "statusSlot NOT exist";
                                        return Ok(output);
                                    }
                                    output.Message = "hardwareConfig NOT exist in this Cyber";
                                    return Ok(output);
                                }
                                output.Message = "Slot NOT exist";
                                return Ok(output);
                            }
                            output.Message = "Room NOT exist";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission for this Function";
                        return Ok(output);
                    }
                    output.Message = "You NOT Login";
                    return Ok(output);
                }
                output.Message = "Cyber NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "EditSlot Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// Xóa đi một Slot
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="slotViewModelIn"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("RemoveSlot")]
        public async Task<ActionResult> RemoveSlot([FromBody] SlotViewModelIn slotViewModelIn)
        {
            var output = new ServiceResponse<int>();
            try
            {
                var cyber = await _cybersRepository.GetCyberById(slotViewModelIn.CyberId);
                if (cyber != null)
                {
                    var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                    if (usernameCrr != null)
                    {
                        var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                        var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                        //check user hiện tại có quyền trong Cyber này không?
                        if (cyber.BossCyberID == currentUser.Id)
                        {
                            var roomCheck = await _roomRepository.GetRoomById(slotViewModelIn.RoomId);
                            if (roomCheck != null)
                            {
                                var slotCheck = await _slotsRepository.GetSlotById(slotViewModelIn.Slot.Id);
                                if (slotCheck != null)
                                {
                                    if ((await _slotsRepository.Delete(slotCheck)) != -1)
                                    {
                                        output.Message = "Successful";
                                        output.Data = 1;
                                        output.Success = true;
                                        return Ok(output);
                                    }
                                    output.Message = "CAN NOT Remove Slot";
                                    return Ok(output);
                                }
                                output.Message = "Slot NOT exist";
                                return Ok(output);
                            }
                            output.Message = "Room NOT exist";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission for this Function";
                        return Ok(output);
                    }
                    output.Message = "You NOT LOGIN";
                    return Ok(output);
                }
                output.Message = "Cyber NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "RemoveSlot Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// lấy slot bằng id 
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="slotId"></param>
        /// <returns></returns>
        [HttpGet("GetSlotById")]
        public async Task<ActionResult> GetSlotById(int slotId)
        {
            var output = new ServiceResponse<Slot>();
            try
            {
                var slot = await _slotsRepository.GetSlotById(slotId);
                if (slot != null)
                {
                    output.Data = slot;
                    output.Message = "Successful";
                    output.Success = false;
                    return Ok(output);
                }
                output.Message = "Slot NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "GetSlotById Exception";
            }
            return Ok(output);
        }

        /// <summary>
        ///   lấy danh sách slot của một phòng
        ///   huynhnd53 02/11/2021
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("GetListSlotByRoom")]
        public async Task<ActionResult> GetListSlotByRoom(PagingOutput<Room> paging)
        {
            var output = new PagingOutput<IEnumerable<Slot>>();
            try
            {
                var room = await _roomRepository.GetRoomById(paging.Data.Id);
                if (room != null)
                {
                    var result = await _slotsRepository.GetAllSlotByRoomId(room.Id, paging.Index, paging.PageSize);
                    if (result.Data.Any())
                    {
                        output = result;
                        output.Message = "Successfully";
                        return Ok(output);
                    }
                    output.Message = "Slot NOT exist";
                    return Ok(output);
                }
                output.Message = "Room NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "GetListSlotByRoom Exception";                    
            }
            return Ok(output);
        }

        /// <summary>
        /// lấy danh sách slot trong một phòng theo trạng thái đã chọn
        ///  huynhnd53 02/11/2021
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="statusSlot"></param>
        /// <returns></returns>
        [HttpGet("GetListSlotByFilter")]
        public async Task<ActionResult> GetListSlotByFilter(PagingOutput<SlotsModelFilter> paging)
        {
            var output = new PagingOutput<IEnumerable<Slot>>();
            try
            {
                var room = await _roomRepository.GetRoomById(paging.Data.RoomId);
                if (room != null)
                {
                    var result = await _slotsRepository.GetSlotByFilter(paging.Data, paging.Index, paging.PageSize);
                    if(result.Data.Any())
                    {
                        output = result;
                        output.Message = "successfully";
                        return Ok(output);
                    }
                    output.Message = "no slot in this room";
                    return Ok(output);
                }
                output.Message = "room not exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "getlistslotbyroom exception";
            }
            return Ok(output);
        }
    }
}
