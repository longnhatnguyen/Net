using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net6.IRepository;
using Net6.Models;

namespace Net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository ProductRepository)
        {
            _productRepository = ProductRepository;
        }

        [HttpGet]
        [Route("AllOrders")]
        public async Task<IActionResult> GetAllTikiOrders([FromBody] PagingRequest paging)
        {
            try
            {
                var result = await _productRepository.GetAllProduct(paging);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("GetAllProductbyCategoryCode")]
        public async Task<IActionResult> GetAllProductbyCategoryCode(int code, [FromQuery] PagingRequest paging)
        {
            try
            {
                var result = await _productRepository.GetAllProductbyCategoryCode(code, paging);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
