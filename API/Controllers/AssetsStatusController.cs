using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTOs.AssetsStatusDTOs;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/assets-status")]
    public class AssetsStatusController : ControllerBase
    {
        private IAssetStatusRepository _repository;
        public AssetsStatusController(IAssetStatusRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetStatusDTO>>> GetAssetsWithStatusAsync(string? status)
        {
            if(status == null)
            {
                return Ok(await _repository.GetAssetsWithStatusAsync());
            }
            else if(!Enum.TryParse<ProcessedStatus>(status, out ProcessedStatus processedStatus))
            {
                return Ok(new List<AssetStatusDTO> ());
            }
            else
            {
                return Ok(await _repository.GetAssetsWithStatusFilterByStatus(processedStatus));
            }
        }

        

    }
}