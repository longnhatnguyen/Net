using CyberBook_API.Dal;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetOrderById(int? id);
        Task<Order> GetOrderByCreatorAndStatus(int creatorId, int statusId);
        Task<Order> ChangeStatusOrder(Order order, int statusId);
        Task<PagingOutput<IEnumerable<Order>>> GetAllOrderByCyberId(int cyberId, int pageIndex, int pageSize);
        Task<PagingOutput<IEnumerable<Order>>> GetAllOrderByUserId(int userId, int pageIndex, int pageSize);

    }
}
