using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net6.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class CustomerController : ControllerBase
    {
        private readonly Learn_DBContext _dbContext;
        public CustomerController(Learn_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        [Authorize(Roles = "user")]
        public IEnumerable<TblCustomer> Get()
        {
            return _dbContext.TblCustomers.ToList();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public TblCustomer Get(int id)
        {
            var xx = _dbContext.TblCustomers.FirstOrDefault(o => o.Id == id);
            return xx;

        }
        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
