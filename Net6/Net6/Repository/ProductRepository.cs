using Microsoft.EntityFrameworkCore;
using Net6.GenericRepository;
using Net6.IRepository;
using Net6.Models;
using System;
using System.Linq;

namespace Net6.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Learn_DBContext _dbContext;

        //private readonly IGenericRepository<TblProduct> _productRepository;

        public ProductRepository(Learn_DBContext learn_DBContext)
        {
            _dbContext = learn_DBContext;
        }

        //private readonly IOrderService _orderService;
        public  async Task<PagingResponse> GetAllProduct(PagingRequest paging)
        {
            IQueryable<TblProduct> data = _dbContext.TblProducts;
            var count = await data.CountAsync();
            var dataPaging = await data.Skip(paging.Skip).Take(paging.Take == 0 ? count : paging.Take).ToListAsync();
            var results = dataPaging.ToArray();
            return new PagingResponse(results, count);
        }

        public async Task<PagingResponse> GetAllProductbyCategoryCode(int code, PagingRequest paging)
        {
            var data =  await (from a in _dbContext.TblProducts join b in _dbContext.Categories on a.CategoryCode equals b.Id where a.CategoryCode == code 
                                                  select new
                                                  {
                                                      product = a,
                                                      category = b
                                                  }).ToListAsync();
            var count = data.Count();
            var dataPaging = data.Skip(paging.Skip).Take(paging.Take == 0 ? count : paging.Take).ToList();
            var results = dataPaging.ToArray();
            return new PagingResponse(results, count);
        }
    }
}
