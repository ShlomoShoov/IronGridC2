using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTOs.ReportsDTOs;

namespace API.Repositories
{
    public interface IReportsRepository
    {
        public  Task<IEnumerable<ReportCriticalAssetsDTO>> GetReportCriticalAssetsAsync();
        public  Task<IEnumerable<ReportUnitAssetsStatusDTO>> GetReportUnitAssetsStatusesAsync(int unitId);
        public Task<IEnumerable<SummaryByUnitDTO>> GetSummaryByUnitAsync();


    }
}