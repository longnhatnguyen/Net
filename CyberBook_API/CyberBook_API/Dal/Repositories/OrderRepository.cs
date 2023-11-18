using CyberBook_API.Enum;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Dal.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public async Task<Order> ChangeStatusOrder(Order order, int statusId)
        {
            var o = (await FindBy(x => x.Id == order.Id)).FirstOrDefault();
            if (o != null)
            {
                o.StatusOrder = statusId;
                var rs = await Update(o, o.Id);
                if (rs != -1)
                {
                    return o;
                }
            }
            return null;
        }

        public async Task<PagingOutput<IEnumerable<Order>>> GetAllOrderByCyberId(int cyberId, int pageIndex, int pageSize)
        {
            var lstOrders = (await FindBy(x => x.CyberId == cyberId)).ToList();

            var orders = (from o in lstOrders
                         orderby o.CreatedDate
                         select o).ToList();

            if (orders.Count > 0)
            {
                var data = orders.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new Order()
                    {
                        Id = x.Id,
                        SlotOrderId = x.SlotOrderId,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        CyberId = x.CyberId,
                        EndAt = x.EndAt,
                        StartAt = x.StartAt,
                        StatusOrder = x.StatusOrder
                    }).ToList();
                int totalPage = orders.Count / pageSize;
                if (orders.Count % pageSize > 0)
                {
                    totalPage = (orders.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<Order>>
                {
                    Index = pageIndex,
                    PageSize = pageSize,
                    TotalItem = orders.Count,
                    TotalPage = totalPage,
                    Data = data
                };
                return paging;
            }
            return null;
        }

        public async Task<PagingOutput<IEnumerable<Order>>> GetAllOrderByUserId(int userId, int pageIndex, int pageSize)
        {
            var lstOrders = (await FindBy(x => x.CreatedBy == userId)).ToList();
            var orders = (from o in lstOrders
                          orderby o.CreatedDate
                          select o).ToList();
            if (orders.Count > 0)
            {
                var data = orders.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new Order()
                    {
                        Id = x.Id,
                        SlotOrderId = x.SlotOrderId,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        CyberId = x.CyberId,
                        EndAt = x.EndAt,
                        StartAt = x.StartAt,
                        StatusOrder = x.StatusOrder
                    }).ToList();
                int totalPage = orders.Count / pageSize;
                if (orders.Count % pageSize > 0)
                {
                    totalPage = (orders.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<Order>>
                {
                    Index = pageIndex,
                    PageSize = pageSize,
                    TotalItem = orders.Count,
                    TotalPage = totalPage,
                    Data = data
                };
                return paging;
            }
            return null;
        }

        public async Task<Order> GetOrderByCreatorAndStatus(int creatorId, int statusId)
        {
            return (await FindBy(x => x.CreatedBy == creatorId && x.StatusOrder == statusId)).FirstOrDefault();
        }

        public async Task<Order> GetOrderById(int? id)
        {
            return (await FindBy(x => x.Id == id)).FirstOrDefault();
        }

    }
}
