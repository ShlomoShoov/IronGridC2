using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Exceptions;
using API.Models.DTOs.ReportsDTOs;
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

        [HttpGet("critical-assets")]
        public async Task<ActionResult<IEnumerable<ReportCriticalAssetsDTO>>> GetReportCriticalAssetsAsync()
        {
            return Ok(await _repository.GetReportCriticalAssetsAsync());
        }
        [HttpGet("unit/{unitId}/assets")]
        public async Task<ActionResult<IEnumerable<ReportUnitAssetsStatusDTO>>> GetReportUnitAssetsStatusesAsync(int unitId)
        {
            try
            {
                return Ok(await _repository.GetReportUnitAssetsStatusesAsync(unitId));
            }
            catch (UnitNotExistsException)
            {
                return  NotFound();
            }
        }
        [HttpGet("summary-by-unit")]
        public async Task<ActionResult<IEnumerable<SummaryByUnitDTO>>> GetSummaryByUnitAsync()
        {
            return Ok(await _repository.GetSummaryByUnitAsync());
        }


    }
}