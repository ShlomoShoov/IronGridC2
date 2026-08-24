using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private IReportsRepository _repository;
        public ReportsController(IReportsRepository repository)
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