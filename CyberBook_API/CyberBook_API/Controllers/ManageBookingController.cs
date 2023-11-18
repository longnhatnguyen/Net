using CyberBook_API.Dal.Repositories;
using CyberBook_API.Enum;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.OrderViewModel;
using CyberBook_API.ViewModel.PagingView;
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
    public class ManageBookingController : ControllerBase
    {
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        private readonly IOrderRepository _orderRepository = new OrderRepository();
        private readonly IAccountsRepository _accountsRepository = new AccountsRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();
        private readonly IRoomRepository _roomRepository = new RoomRepository();
        private readonly ICyberAccountRepository _cyberAccountRepository = new CyberAccountRepository();
        private readonly ISlotsRepository _slotsRepository = new SlotsRepository();
        private readonly IStatusSlotRepository _statusSlotRepository = new StatusSlotRepository();


        /// <summary>
        /// Lấy danh sách Order của một Cyber bằng ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetListBookingByCyberId")]
        public async Task<ActionResult> GetListBookingByCyberId([FromBody] PagingOutput<int> paging)
        {
            var output = new PagingOutput<IEnumerable<Order>>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                    var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                    var cyber = await _cybersRepository.GetCyberById(paging.Data);
                    if (cyber != null)
                    {
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)))
                        {
                            var result = await _orderRepository.GetAllOrderByCyberId(paging.Data, paging.Index, paging.PageSize);
                            if (result.Data.Any())
                            {
                                output = result;
                                output.Message = "successfull";
                                return Ok(output);
                            }
                            output.Message = "No Orders in this Cyber";
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
            catch (Exception ex)
            {
                output.Message = ex.Message;
            }
            return Ok(output);
        }

        [HttpGet("GetListBookingByUserId")]
        public async Task<ActionResult> GetListBookingByUserId([FromBody] PagingOutput<string> paging)
        {
            var output = new PagingOutput<IEnumerable<Order>>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                    var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                    var result = await _orderRepository.GetAllOrderByUserId(currentUser.Id, paging.Index, paging.PageSize);
                    if (result.Data.Any())
                    {
                        output = result;
                        output.Message = "successfull";
                        return Ok(output);
                    }
                    output.Message = "No Orders in this Cyber";
                    return Ok(output);
                }
                output.Message = "You NOT LOGIN";
                return Ok(output);
            }
            catch (Exception ex)
            {
                output.Message = ex.Message;
            }
            return Ok(output);
        }

        /// <summary>
        /// lấy danh sách của một Order bằng ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBookingById")]
        public async Task<ActionResult> GetBookingById(int orderId)
        {
            var output = new ServiceResponse<OrderViewModel>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                    var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);

                    var order = await _orderRepository.GetOrderById(orderId);
                    if (order != null)
                    {
                        var cyber = await _cybersRepository.GetCyberById(order.CyberId);
                        if (cyber != null)
                        {
                            //check user hiện tại có quyền trong Cyber này không?
                            //Hoặc là chủ Cyber của order này chủ sở hữu của order này
                            if (cyber.BossCyberID == currentUser.Id || order.CreatedBy == currentUser.Id)
                            {
                                string[] lstSlotId = order.SlotOrderId.Split('|');
                                var lstSlots = new List<Slot>();
                                foreach (var s in lstSlotId)
                                {
                                    int i = 0;
                                    var slot = (await _slotsRepository.FindBy(x => x.Id == Convert.ToInt32(s[i].ToString()))).FirstOrDefault();
                                    if (slot != null)
                                    {
                                        lstSlots.Add(slot);
                                        i++;
                                    }
                                }
                                var orderViewOut = new OrderViewModel
                                {
                                    Id = order.Id,
                                    StartAt = order.StartAt,
                                    EndAt = order.EndAt,
                                    CreatedDate = order.CreatedDate,
                                    CreatedBy = order.CreatedBy,
                                    StatusOrder = order.StatusOrder,
                                    CyberId = order.CyberId,
                                    SlotOrderId = lstSlots
                                };
                                output.Data = orderViewOut;
                                output.Message = "successfull";
                                output.Success = true;
                                return Ok(output);
                            }
                            output.Message = "You NOT permission this Function";
                            return Ok(output);
                        }
                        output.Message = "Cyber NOT Exist";
                        return Ok(output);
                    }
                    output.Message = "Order NOT exist";
                    return Ok(output);
                }
                output.Message = "You NOT LOGIN";
                return Ok(output);
            }
            catch (Exception ex)
            {
                output.Message = ex.Message;
            }
            return Ok(output);
        }

        /// <summary>
        /// tạo mới một order
        /// </summary>
        /// <param name="orderViewModel"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost("CreateNewOrder")]
        public async Task<ActionResult> CreateNewOrder([FromBody] OrderViewModel orderViewModel)
        {
            var output = new ServiceResponse<Order>();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                    var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                    var cyber = await _cybersRepository.GetCyberById(orderViewModel.CyberId);
                    if (cyber != null)
                    {
                        //user không có tài khoản ở quán không được book 
                        //var cyberAccount = (await _cyberAccountRepository.FindBy(x => x.UserId == currentUser.Id && x.CyberId == orderViewModel.CyberId)).FirstOrDefault();
                        //if(cyberAccount != null)
                        //{
                        //}
                        var slotOrderIdToString = string.Empty;
                        //check Slot in list slot of order has exist
                        foreach (var s in orderViewModel.SlotOrderId)
                        {
                            if (!slotOrderIdToString.Equals("") || !String.IsNullOrEmpty(slotOrderIdToString))
                            {
                                slotOrderIdToString += "|";
                            }
                            //var slotCheck = (await _slotsRepository.FindBy(x => x.Id == s.Id && x.StatusId == Convert.ToInt32(SlotsEnum.SlotStatus.Ready))).FirstOrDefault();
                            var slotCheck = await _slotsRepository.IsSlotReady(s.Id);
                            if (slotCheck == null)
                            {
                                output.Message = "Slot Not Ready for Book";
                                return Ok(output);
                            }
                            slotOrderIdToString += s.Id;
                        }
                        var sttOrder = (await _statusSlotRepository.FindBy(x => x.Id == orderViewModel.StatusOrder)).FirstOrDefault();
                        if (sttOrder == null)
                        {
                            output.Message = "Status Slot NOT exist";
                            return Ok(output);
                        }

                        //không cho tạo mới 1 order khi đang có 1 order đang chờ duyệt
                        // Status = 2 = PENDING
                        var crrOrderCheck = await _orderRepository.GetOrderByCreatorAndStatus(currentUser.Id, Convert.ToInt32(OrdersEnum.StatusOrder.Pending));
                        if (crrOrderCheck == null)
                        {
                            var orderTempt = new Order
                            {
                                StartAt = orderViewModel.StartAt,
                                EndAt = orderViewModel.EndAt,
                                CreatedDate = DateTime.Now,
                                CreatedBy = currentUser.Id,
                                StatusOrder = Convert.ToInt32(OrdersEnum.StatusOrder.Pending), //PENDING  trạng thái của Order đang chờ duyệt
                                CyberId = cyber.Id,
                                SlotOrderId = slotOrderIdToString
                            };
                            var newOder = (await _orderRepository.Create(orderTempt));
                            if (newOder != null)
                            {
                                foreach (var s in orderViewModel.SlotOrderId)
                                {
                                    //var slotCheck = (await _slotsRepository.FindBy(x => x.Id == s.Id && x.StatusId == Convert.ToInt32(SlotsEnum.SlotStatus.Ready))).FirstOrDefault();
                                    var slotCheck = await _slotsRepository.IsSlotReady(s.Id);
                                    if (slotCheck != null)
                                    {
                                        //update status slot
                                        //Status = 4 = đã có người book
                                        slotCheck.StatusId = Convert.ToInt32(SlotsEnum.SlotStatus.Booked);
                                        if ((await _slotsRepository.Update(slotCheck, slotCheck.Id) == -1))
                                        {
                                            output.Message = "update status slot Fail";
                                            return Ok(output);
                                        }
                                    }
                                }
                                output.Data = newOder;
                                output.Message = "Crete new Order OK";
                                output.Success = true;
                                return Ok(output);
                            }
                            output.Message = "Create order FAIL";
                            return Ok(output);
                        }
                        output.Message = "You can't create new Order when has another Order";
                        return Ok(output);
                    }
                    output.Message = "Cyber Not exist";
                    return Ok(output);
                }
                output.Message = "You NOT Login";
                return Ok(output);
            }
            catch (Exception e)
            {
                output.Message = e.Message;
            }
            return Ok(output);
        }

        /// <summary>
        /// Cyber Admin thay đổi trạng thái của 1 order
        /// </summary>
        /// <param name="orderIn"></param>
        /// <returns></returns>
        [HttpPut("ApproveOrder")]
        public async Task<ActionResult> ApproveOrder([FromBody] OrderViewModel orderViewModelIn)
        {
            var output = new ServiceResponse<OrderViewModel>();
            var data = new OrderViewModel();
            try
            {
                //check user hiện tại
                var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                if (usernameCrr != null)
                {
                    var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                    var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                    var orderTemp = await _orderRepository.GetOrderById(orderViewModelIn.Id);
                    //lấy cyber trong đơn Order này 
                    var cyber = await _cybersRepository.GetCyberById(orderViewModelIn.CyberId);
                    if (cyber != null)
                    {
                        //check user hiện tại có quyền trong Cyber này không
                        if ((await _cybersRepository.IsBossCyber(currentUser.Id, cyber.Id)) && cyber.Id == orderTemp.CyberId)
                        {
                            //Set Status của Order là 1 
                            //Status Order = 1 = APPROVE 
                            //orderTemp.StatusOrder = 1;
                            var rs = await _orderRepository.ChangeStatusOrder(orderTemp, Convert.ToInt32(OrdersEnum.StatusOrder.Approve));
                            if (rs != null)
                            {
                                string[] lstSlotsId = orderTemp.SlotOrderId.Split('|');
                                var lstSlots = new List<Slot>();
                                foreach (var s in lstSlotsId)
                                {
                                    int i = 0;
                                    //lấy ra list các slot đang được book bởi order hiện tại
                                    //Slot Status = 2 = RẢNH
                                    var slot = (await _slotsRepository.FindBy(x => x.Id == Convert.ToInt32(s[i].ToString()))).FirstOrDefault();
                                    if (slot != null)
                                    {
                                        lstSlots.Add(slot);
                                        i++;
                                    }
                                }
                                data.Id = rs.Id;
                                data.StartAt = rs.StartAt;
                                data.EndAt = rs.EndAt;
                                data.CreatedDate = rs.CreatedDate;
                                data.CreatedBy = rs.CreatedBy;
                                data.StatusOrder = rs.StatusOrder;
                                data.CyberId = rs.CyberId;
                                data.SlotOrderId = lstSlots;

                                output.Success = true;
                                output.Message = "APPROVE order successfull";
                                output.Data = data;
                                return Ok(output);
                            }
                            output.Message = "APPROVE order FAIL";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission this Function";
                        return Ok(output);
                    }
                    output.Message = "You NOT permission this Function";
                    return Ok(output);
                }
                output.Message = "You NOT LOGIN";
                return Ok(output);
            }
            catch (Exception ex)
            {
                output.Message = ex.Message;
            }
            return Ok(output);
        }

        /// <summary>
        /// Cyber Admin thay đổi trạng thái của 1 order
        /// </summary>
        /// <param name="orderIn"></param>
        /// <returns></returns>
        [HttpPut("RejectOrder")]
        public async Task<ActionResult> RejectOrder([FromBody] OrderViewModel orderViewModelIn)
        {
            var output = new ServiceResponse<OrderViewModel>();
            var data = new OrderViewModel();
            try
            {
                //tìm cyber trong order có tồn tại 
                var cyber = await _cybersRepository.GetCyberById(orderViewModelIn.CyberId);
                if (cyber != null)
                {
                    //check user hiện tại
                    var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                    if (usernameCrr != null)
                    {
                        var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                        var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                        var orderTemp = await _orderRepository.GetOrderById(orderViewModelIn.Id);
                        //check user hiện tại có quyền trong Cyber này không?  
                        if (currentUser.Id == cyber.BossCyberID && cyber.Id == orderTemp.CyberId)
                        {
                            string[] lstSlotId = orderTemp.SlotOrderId.Split('|');
                            var lstSlots = new List<Slot>();
                            foreach (var s in lstSlotId)
                            {
                                int i = 0;
                                var slot = (await _slotsRepository.FindBy(x => x.Id == Convert.ToInt32(s[i].ToString()))).FirstOrDefault();
                                if (slot != null)
                                {
                                    //trả lại trạng thái của các slot là RẢNH, sẵn sàng để cho người khác book 
                                    //Slot Status = 2 = RẢNH
                                    slot.StatusId = Convert.ToInt32(SlotsEnum.SlotStatus.Ready);
                                    await _slotsRepository.Update(slot, slot.Id);
                                    lstSlots.Add(slot);
                                    i++;
                                }
                            }
                            //Set Status của Order là 3 
                            //Status Order = 3 = REJECT 
                            //orderTemp.StatusOrder = Convert.ToInt32(OrdersEnum.StatusOrder.Reject);
                            var rs = await _orderRepository.ChangeStatusOrder(orderTemp, Convert.ToInt32(OrdersEnum.StatusOrder.Reject));
                            if (rs != null)
                            {
                                data.Id = rs.Id;
                                data.StartAt = rs.StartAt;
                                data.EndAt = rs.EndAt;
                                data.CreatedDate = rs.CreatedDate;
                                data.CreatedBy = rs.CreatedBy;
                                data.StatusOrder = rs.StatusOrder;
                                data.CyberId = rs.CyberId;
                                data.SlotOrderId = lstSlots;

                                output.Success = true;
                                output.Message = "REJECT order successfull";
                                output.Data = data;
                                return Ok(output);
                            }
                            output.Message = "FAIL to REJECT";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission this Function";
                        return Ok(output);
                    }
                    output.Message = "You NOT LOGIN";
                    return Ok(output);
                }
                output.Message = "Cyber NOT exist";
                return Ok(output);
            }
            catch (Exception e)
            {
                output.Message = e.Message;
            }
            return Ok(output);
        }

        /// <summary>
        /// người dùng cancel order của mình
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("CancelOrder")]
        public async Task<ActionResult> CancelOrder([FromBody] OrderViewModel orderViewModelIn)
        {
            var output = new ServiceResponse<OrderViewModel>();
            var data = new OrderViewModel();
            try
            {
                var cyber = await _cybersRepository.GetCyberById(orderViewModelIn.CyberId);
                if (cyber != null)
                {
                    //check user hiện tại
                    var usernameCrr = User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
                    if (usernameCrr != null)
                    {
                        var accountCrr = await _accountsRepository.GetAccountByUsername(usernameCrr);
                        var currentUser = await _usersRepository.GetUserByAccountID(accountCrr.Id);
                        var orderTemp = await _orderRepository.GetOrderById(orderViewModelIn.Id);
                        //check user hiện tại có quyền trong Cyber này không?
                        if (orderTemp.CreatedBy == currentUser.Id)
                        {
                            string[] lstSlotId = orderTemp.SlotOrderId.Split('|');
                            var lstSlots = new List<Slot>();
                            foreach (var s in lstSlotId)
                            {
                                int i = 0;
                                //trả lại trạng thái của các slot là RẢNH, sẵn sàng để cho người khác book 
                                //Slot Status = 2 = RẢNH
                                var slot = (await _slotsRepository.FindBy(x => x.Id == Convert.ToInt32(s[i].ToString()))).FirstOrDefault();
                                if (slot != null)
                                {
                                    slot.StatusId = Convert.ToInt32(SlotsEnum.SlotStatus.Ready);
                                    await _slotsRepository.Update(slot, slot.Id);
                                    lstSlots.Add(slot);
                                    i++;
                                }
                            }
                            //Set Status của Order là 4
                            // người dùng hủy bản Order
                            //Status Order = 4 = CANCEL 
                            //orderTemp.StatusOrder = 4;
                            var rs = await _orderRepository.ChangeStatusOrder(orderTemp, Convert.ToInt32(OrdersEnum.StatusOrder.Cancel));
                            if (rs != null)
                            {
                                data.Id = rs.Id;
                                data.StartAt = rs.StartAt;
                                data.EndAt = rs.EndAt;
                                data.CreatedDate = rs.CreatedDate;
                                data.CreatedBy = rs.CreatedBy;
                                data.StatusOrder = rs.StatusOrder;
                                data.CyberId = rs.CyberId;
                                data.SlotOrderId = lstSlots;

                                output.Success = true;
                                output.Data = data;
                                output.Message = "Cancel Order Successfull";
                                return Ok(output);
                            }
                            output.Message = "FAIL to Cancel";
                            return Ok(output);
                        }
                        output.Message = "You NOT permission this Function";
                        return Ok(output);
                    }
                    output.Message = "You NOT LOGIN";
                    return Ok(output);
                }
                output.Message = "Cyber NOT exist";
                return Ok(output);
            }
            catch (Exception e)
            {
                output.Message = e.Message;
            }
            return Ok(output);
        }

        [HttpGet("CheckSplit")]
        public async Task<ActionResult> CheckSplit(int id)
        {
            var output = new ServiceResponse<IEnumerable<Slot>>();

            var order = (await _orderRepository.FindBy(x => x.Id == id)).FirstOrDefault();
            string[] lstSlotId = order.SlotOrderId.Split('|');
            var lstSlots = new List<Slot>();
            foreach (var s in lstSlotId)
            {
                int i = 0;
                var slot = (await _slotsRepository.FindBy(x => x.Id == Convert.ToInt32(s[i].ToString()))).FirstOrDefault();
                if (slot != null)
                {
                    lstSlots.Add(slot);
                    i++;
                }
            }
            output.Data = lstSlots;
            return Ok(output);
        }
    }
}
