using CyberBook_API.Dal.Repositories;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.AccountView;
using CyberBook_API.ViewModel.PagingView;
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
    public class SearchController : ControllerBase
    {
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        [HttpGet("GetAllCybers")]
        public async Task<ActionResult> GetAllCybers([FromBody]PagingOutput<string> paging)
        {
            var output = new PagingOutput<IEnumerable<Cyber>>();
            try
            {
                var result = await _cybersRepository.GetAllCyberAvaiable(paging.Index, paging.PageSize);
                if (result.Data.Any())
                {
                    output.Message = "Success";
                    output.Index = result.Index;
                    output.PageSize = result.PageSize;
                    output.TotalItem = result.TotalItem;
                    output.TotalPage = result.TotalPage;
                    output.Data = result.Data;
                    return Ok(output);
                }
                output.Message = "No Cyber";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "Exception get All Cyber";
            }
            return Ok(output);
        }

        [HttpGet("SearchCyberByName")]
        public async Task<ActionResult> SearchCyberByName([FromBody] PagingOutput<string> paging)
        {
            var output = new PagingOutput<IEnumerable<Cyber>>();
            try
            {
                var result = await _cybersRepository.GetCyberByName(paging.Data, paging.Index, paging.PageSize);
                if (result.Data.Any())
                {
                    output.Message = "Success";
                    output.Index = result.Index;
                    output.PageSize = result.PageSize;
                    output.TotalItem = result.TotalItem;
                    output.TotalPage = result.TotalPage;
                    output.Data = result.Data;
                    return Ok(output);
                }
                output.Message = "No Cyber";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "Exception";
            }
            return Ok(output);
        }

        [HttpGet("GetCyberById")]
        public async Task<ActionResult> GetCyber(int id)
        {
            var output = new ServiceResponse<Cyber>();
            try
            {
                var cybers = await _cybersRepository.GetCyberById(id);
                if (cybers != null)
                {
                    output.Success = true;
                    output.Message = "Success";
                    output.Data = cybers;
                }
                output.Message = "No Cyber";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "Exception";
            }
            return Ok(output);
        }

        [HttpGet("ViewCyberByRate")]
        public async Task<ActionResult> SearchCyberByRate([FromBody] PagingOutput<string> paging)
        {
            var output = new PagingOutput<IEnumerable<Cyber>>();
            try
            {
                var result = await _cybersRepository.GetCyberByRatePoint(paging.Index, paging.PageSize);
                if (result.Data.Any())
                {
                    output.Message = "Success";
                    output.Index = result.Index;
                    output.PageSize = result.PageSize;
                    output.TotalItem = result.TotalItem;
                    output.TotalPage = result.TotalPage;
                    output.Data = result.Data;
                    return Ok(output);
                }
                output.Message = "No Cyber";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "Exception get All Cyber";
            }
            return Ok(output);
        }

    }
}
