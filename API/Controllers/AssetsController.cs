using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models.DTOs.AssetsDTOs;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private IAssetsRepository _repository;
        public AssetsController(IAssetsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDTO>> GetAssetByIdAsync(int id)
        {
            AssetDTO? asset = await _repository.GetAssetByIdAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            return Ok(asset);
        }
        [HttpPost("units")]
        public async Task<ActionResult> CreateUnitAsync(CreateUnitDto unitDto)
        {
            await _repository.CreateUnitAsync(unitDto);
            return new StatusCodeResult(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssetAsync(int id, UpdateAssetDTO updatedAsset)
        {
            AssetDTO? asset = await _repository.UpdateAssetAsync(id, updatedAsset);
            if (asset == null)
            {
                return NotFound();
            }
            return Ok(asset);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetAsync(int id)
        {
            if (!await _repository.DeleteAssetAsync(id))
            {
                return NoContent();
            }
            return Ok();
        }


    }
}