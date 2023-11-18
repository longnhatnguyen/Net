using CyberBook_API.Dal.Repositories;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.AccountView;
using CyberBook_API.ViewModel.PagingView;
using CyberBook_API.ViewModel.RoomViewModel;
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
    [ApiController]
    public class RoomController : ControllerBase
    {

        private readonly ISlotsRepository _slotsRepository = new SlotsRepository();
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        private readonly IAccountsRepository _accountsRepository = new AccountsRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();
        private readonly IRoomRepository _roomRepository = new RoomRepository();
        private readonly IRoomTypeRepository _roomTypeRepository = new RoomTypeRepository();

        /// <summary>
        /// tạo mới 1 Room
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="roomViewModelIn"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("CreateNewRoom")]
        public async Task<ActionResult> CreateNewRoom([FromBody] Room roomViewModelIn)
        {
            var output = new ServiceResponse<Room>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                var cyber = await _cybersRepository.GetCyberById(roomViewModelIn.CyberId);
                if (cyber != null)
                {
                    //check user hiện tại có quyền trong Cyber này không?
                    if (await _cybersRepository.IsBossCyber(currentUser.Id, cyber.BossCyberID))
                    {
                        var roomType = await _roomTypeRepository.GetRoomTypeById(roomViewModelIn.RoomType);
                        if (roomType != null)
                        {
                            if (!roomViewModelIn.RoomName.Equals(""))
                            {
                                var roomTempt = new Room
                                {
                                    CyberId = cyber.Id,
                                    RoomName = roomViewModelIn.RoomName,
                                    RoomType = roomViewModelIn.RoomType,
                                    RoomPosition = roomViewModelIn.RoomPosition,
                                    PriceRoom = roomViewModelIn.PriceRoom
                                };
                                var newRoom = await _roomRepository.Create(roomTempt);
                                if (newRoom != null)
                                {
                                    output.Data = newRoom;
                                    output.Message = "Create New Room Successfully";
                                    output.Success = true;
                                    return Ok(output);
                                }
                                output.Message = "Create New Room Fail";
                                return Ok(output);
                            }
                            output.Message = "RoomName CAN NOT empty";
                            return Ok(output);
                        }
                        output.Message = "RoomType not exist";
                        return Ok(output);
                    }
                    output.Message = "You NOT permission for this Function";
                    return Ok(output);
                }
                output.Message = "Cyber NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "CreateNewRoom Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// Sửa thông tin 1 Room 
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="roomViewModelIn"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("EditRoom")]
        public async Task<ActionResult> EditRoom([FromBody] Room roomViewModelIn)
        {
            var output = new ServiceResponse<Room>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                var cyber = await _cybersRepository.GetCyberById(roomViewModelIn.CyberId);
                if (cyber != null)
                {
                    //check user hiện tại có quyền trong Cyber này không?
                    if (await _cybersRepository.IsBossCyber(currentUser.Id, cyber.BossCyberID))
                    {
                        //var room = (await _roomRepository.FindBy(x => x.Id == roomViewModelIn.Room.Id)).FirstOrDefault();
                        var room = await _roomRepository.GetRoomById(roomViewModelIn.Id);
                        if (room != null)
                        {
                            //var roomType = (await _roomTypeRepository.FindBy(x => x.Id == roomViewModelIn.Room.RoomType)).FirstOrDefault();
                            var roomType = await _roomTypeRepository.GetRoomTypeById(roomViewModelIn.RoomType);
                            if (roomType != null)
                            {
                                if (!roomViewModelIn.RoomName.Equals(""))
                                {
                                    room.RoomName = roomViewModelIn.RoomName;
                                    room.RoomPosition = roomViewModelIn.RoomPosition;
                                    room.RoomType = roomViewModelIn.RoomType;
                                    room.PriceRoom = roomViewModelIn.PriceRoom;
                                    var newRoom = await _roomRepository.Update(room, room.Id);
                                    if (newRoom != -1)
                                    {
                                        output.Data = room;
                                        output.Message = "Create New Room Successfully";
                                        output.Success = true;
                                        return Ok(output);
                                    }
                                    output.Message = "Create New Room Fail";
                                    return Ok(output);
                                }
                                output.Message = "RoomName CAN NOT empty";
                                return Ok(output);
                            }
                            output.Message = "RoomType NOT exist";
                            return Ok(output);
                        }
                        output.Message = "Room NOT exist";
                        return Ok(output);
                    }
                    output.Message = "You NOT permission for this Function";
                    return Ok(output);
                }
                output.Message = "Cyber NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "EditRoom Exception";
            }
            return Ok(output);
        }

        [Authorize]
        [HttpPost("EditSizeRoom-Allow")]
        public async Task<ActionResult> EditSizeRoomAllow([FromBody] Room roomViewModelIn)
        {
            var output = new ServiceResponse<Room>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                var cyber = await _cybersRepository.GetCyberByBossCyberId(currentUser.Id);
                if (cyber != null)
                {
                    //check user hiện tại có quyền trong Cyber này không?
                    if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                    {
                        var room = await _roomRepository.GetRoomById(roomViewModelIn.Id);
                        if (room != null)
                        {
                            var roomType = await _roomTypeRepository.GetRoomTypeById(roomViewModelIn.RoomType);
                            if (roomType != null)
                            {
                                if (!roomViewModelIn.RoomName.Equals(""))
                                {
                                    var newRoom = await _roomRepository.EditRoomSizeAllow(room.Id, roomViewModelIn.MaxX, roomViewModelIn.MaxY);
                                    if (newRoom != null)
                                    {
                                        output.Data = room;
                                        output.Message = "Update Size Room Successfully";
                                        output.Success = true;
                                        return Ok(output);
                                    }
                                    output.Message = "Size Room Room Fail";
                                    return Ok(output);
                                }
                                output.Message = "RoomName CAN NOT empty";
                                return Ok(output);
                            }
                            output.Message = "RoomType NOT exist";
                            return Ok(output);
                        }
                        output.Message = "Room NOT exist";
                        return Ok(output);
                    }
                    output.Message = "You NOT permission for this Function";
                    return Ok(output);
                }
                output.Message = "Cyber NOT Exist";
                return Ok(output);
            }

            catch (Exception)
            {
                output.Message = "EditSizeRoom Exception";
            }
            return Ok(output);
        }

        [Authorize]
        [HttpPost("EditSizeRoom")]
        public async Task<ActionResult> EditSizeRoom([FromBody] Room roomViewModelIn)
        {
            var output = new ServiceResponse<Room>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                var cyber = await _cybersRepository.GetCyberByBossCyberId(currentUser.Id);
                //check user hiện tại có quyền trong Cyber này không?
                if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                {
                    var room = await _roomRepository.GetRoomById(roomViewModelIn.Id);
                    if (room != null)
                    {
                        var roomType = await _roomTypeRepository.GetRoomTypeById(roomViewModelIn.RoomType);
                        if (roomType != null)
                        {
                            if (!roomViewModelIn.RoomName.Equals(""))
                            {
                                var newRoom = await _roomRepository.EditRoomSize(room.Id, roomViewModelIn.MaxX, roomViewModelIn.MaxY);
                                if (newRoom != null)
                                {
                                    output.Data = room;
                                    output.Message = "Update Size Room Successfully";
                                    output.Success = true;
                                    return Ok(output);
                                }
                                output.Message = "Size Room Room Fail";
                                return Ok(output);
                            }
                            output.Message = "RoomName CAN NOT empty";
                            return Ok(output);
                        }
                        output.Message = "RoomType NOT exist";
                        return Ok(output);
                    }
                    output.Message = "Room NOT exist";
                    return Ok(output);
                }
                output.Message = "You NOT permission for this Function";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "EditSizeRoom Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// Xóa đi 1 phòng 
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="roomViewModelIn"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("RemoveRoom")]
        public async Task<ActionResult> RemoveRoom([FromBody] Room roomViewModelIn)
        {
            var output = new ServiceResponse<int>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                var cyber = await _cybersRepository.GetCyberByBossCyberId(currentUser.Id);
                //check user hiện tại có quyền trong Cyber này không?
                if (cyber.BossCyberID == currentUser.Id)
                {
                    var room = await _roomRepository.GetRoomById(roomViewModelIn.Id);
                    if (room != null)
                    {
                        if ((await _roomRepository.Delete(room)) != -1)
                        {
                            output.Message = "Successful";
                            output.Data = 1;
                            output.Success = true;
                            return Ok(output);
                        }
                        output.Message = "CAN NOT Remove Room";
                        return Ok(output);
                    }
                    output.Message = "Room NOT exist";
                    return Ok(output);
                }
                output.Message = "You NOT permission for this Function";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "RemoveRoom Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// lấy thông tin của 1 room bằng id
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetRoomById")]
        public async Task<ActionResult> GetRoomById(int roomId)
        {
            var output = new ServiceResponse<Room>();
            try
            {
                var room = await _roomRepository.GetRoomById(roomId);
                if (room != null)
                {
                    output.Data = room;
                    output.Message = "Successfully";
                    output.Success = true;
                    return Ok(output);
                }
                output.Message = "Room NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "GetRoomById Exception";
            }
            return Ok(output);
        }

        /// <summary>
        /// lấy danh sách phòng của 1 cyber
        /// huynhnd53 02/11/2021
        /// </summary>
        /// <param name="cyberId"></param>
        /// <returns></returns>
        [HttpGet("GetListRoomByCyberId")]
        public async Task<ActionResult> GetListRoomByCyberId([FromBody]PagingOutput<int> paging)
        {
            var output = new PagingOutput<IEnumerable<Room>>();
            try
            {
                var cyber = await _cybersRepository.GetCyberById(paging.Data);
                if (cyber != null)
                {
                    var result = await _roomRepository.GetListRoomByCyberId(paging.Data, paging.Index, paging.PageSize);
                    if (result.Data.Any())
                    {
                        output = result;
                        output.Message = "Successfully";
                        return Ok(output);
                    }
                    output.Message = "Room NOT exist";
                    return Ok(output);
                }
                output.Message = "Cyber NOT exist";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "GetRoomById Exception";
            }
            return Ok(output);
        }
    }
}
