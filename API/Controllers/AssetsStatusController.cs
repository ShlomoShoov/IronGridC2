using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Test()
        {
            return Ok("Hello world!");
        }
    }
}