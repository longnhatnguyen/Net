using CyberBook_API.Dal.Repositories;
using CyberBook_API.Enum;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Controllers
{
    [Route("api/Admins")]
    [ApiController]
    public class AdminCyberController : ControllerBase
    {
        private readonly IAdminsRepository _adminsRepository = new AdminsRepository();
        private readonly ICybersRepository _cybersRepository = new CybersRepository();

        [HttpGet("ListCyber")]
        //[Authorize]
        public async Task<ServiceResponse<IEnumerable<Cyber>>> ListCyber()
        {
            var output = new ServiceResponse<IEnumerable<Cyber>>();
            var outt = await _cybersRepository.GetAll();

            output.Data = outt;
            output.Success = true;
            output.Message = "Get OK";
            return output;
        }

        [HttpGet("ViewDetailCyber")]
        public async Task<ActionResult> ViewDetailCyber(int id)
        {            
            var output = new ServiceResponse<Cyber>();

            try
            {
                var detailCyber = (await _cybersRepository.FindBy(x => x.Id == id)).FirstOrDefault();
                if (detailCyber != null)
                {
                    output.Data = detailCyber;
                    output.Message = "Success";
                    output.Success = true;
                    return Ok(output);
                }
                else
                {
                    output.Message = "can not find";
                    return Ok(output);
                }            
            } catch (Exception ex)
            {
                output.Message = ex.Message;
            }

            return Ok(output);
        }

        // 
        [HttpPost("LockCyber")]
        public async Task<ActionResult> LockCyber(int id)
        {
            var output = new ServiceResponse<Cyber>();

            try
            {
                var cyber = (await _cybersRepository.FindBy(x => x.Id == id)).FirstOrDefault();
                if(cyber != null)
                {
                    cyber.status = Convert.ToInt32(CybersEnum.CybersStatus.Deactivate);
                    return Ok(output);
                }
            } catch (Exception ex)
            {
                output.Message = "Error: " + ex.Message;
            }
            return Ok(output);
        }

        

    }
}
