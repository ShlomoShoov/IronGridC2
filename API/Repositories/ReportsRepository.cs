using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Exceptions;
using API.Models.DTOs.ReportsDTOs;
using Consumer.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private IronGridDbContext _context;

        public ReportsRepository(IronGridDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReportCriticalAssetsDTO>> GetReportCriticalAssetsAsync()
        {
            IQueryable<ReportCriticalAssetsDTO> query = _context.Assets.
                                                        Where(a=> a.AssetLiveStatus != null && (a.AssetLiveStatus.ProcessedStatus == Models.ProcessedStatus.Warning || !a.AssetLiveStatus.IsVerified))
                                                        .Select(a=> new ReportCriticalAssetsDTO
                                                        {
                                                            AssetId = a.Id,
                                                            AssetSerial = a.AssetSerial,
                                                            AssetType = a.AssetType,
                                                            IsVerified = a.AssetLiveStatus!.IsVerified,
                                                            LastUpdate = a.AssetLiveStatus!.LastUpdate,
                                                            ProcessedStatus = a.AssetLiveStatus!.ProcessedStatus,
                                                            Sector = a.Unit.Sector,
                                                            UnitName = a.Unit.UnitName
                                                        });
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ReportUnitAssetsStatusDTO>> GetReportUnitAssetsStatusesAsync(int unitId)
        {
            if(!await _context.Units.AnyAsync(u=> u.Id == unitId))
            {
                throw new UnitNotExistsException();
            }
            
            IQueryable<ReportUnitAssetsStatusDTO> query = _context.Assets.Where(a=> a.UnitId == unitId)
                                                        .Select(a=> new ReportUnitAssetsStatusDTO
                                                        {
                                                            AssetId = a.Id,
                                                            AssetSerial = a.AssetSerial,
                                                            AssetType = a.AssetType,
                                                            IsVerified = a.AssetLiveStatus != null ? a.AssetLiveStatus.IsVerified : null,
                                                            LastUpdate = a.AssetLiveStatus != null ? a.AssetLiveStatus.LastUpdate : null,
                                                            ProcessedStatus = a.AssetLiveStatus != null ? a.AssetLiveStatus.ProcessedStatus : null
                                                        });
            return await query.ToListAsync();
        }


        public async Task<IEnumerable<SummaryByUnitDTO>> GetSummaryByUnitAsync()
        {
            IQueryable<SummaryByUnitDTO> query = _context.Units.Select(u=> new SummaryByUnitDTO
            {
                UnitId = u.Id,
                UnitName = u.UnitName,
                Sector = u.Sector,
                StableAssets = u.Assets.Where(a=> a.AssetLiveStatus!= null && a.AssetLiveStatus.ProcessedStatus == Models.ProcessedStatus.Stable).Count(),
                TotalAssets = u.Assets.Count(),
                WarningAssets = u.Assets.Where(a=> a.AssetLiveStatus != null && a.AssetLiveStatus.ProcessedStatus == Models.ProcessedStatus.Warning).Count(),
                UnverifiedAssets = u.Assets.Where(a => a.AssetLiveStatus != null && !a.AssetLiveStatus.IsVerified).Count()
            });
            return await query.ToListAsync();
        }
    }
}