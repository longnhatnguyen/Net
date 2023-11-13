using Net6.GenericRepository;
using Net6.Models;
using System.Security.Principal;

namespace Net6.IRepository
{
    public interface IProductRepository
    {
        Task<PagingResponse> GetAllProduct(PagingRequest paging);
        Task<PagingResponse> GetAllProductbyCategoryCode(int code, PagingRequest paging);

    }
}
