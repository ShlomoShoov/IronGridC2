using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTOs.AssetsStatusDTOs;
using Consumer.DAL;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AssetStatusRepository : IAssetStatusRepository
    {
        private IronGridDbContext _context;

        public AssetStatusRepository(IronGridDbContext context)
        {
            _context = context;
        }

        private IQueryable<AssetStatusDTO> _DtoizeAssetsWithStatus(IQueryable<Asset> assets)
        {
            return assets.Select(a=> new AssetStatusDTO
            {
                Id = a.Id,
                UnitId = a.UnitId,
                AssetSerial = a.AssetSerial,
                AssetType = a.AssetType,
                AssetStatusLive = a.AssetLiveStatus != null ? new AssetStatusLiveDTO
                {
                    AssetId = a.AssetLiveStatus!.AssetId,
                    AssetType = a.AssetLiveStatus!.AssetType,
                    RawValue = a.AssetLiveStatus!.RawValue,
                    IsVerified = a.AssetLiveStatus!.IsVerified,
                    LastUpdate = a.AssetLiveStatus!.LastUpdate,
                    ProcessedStatus = a.AssetLiveStatus!.ProcessedStatus
                } : null
            });
        }

        public async Task<IEnumerable<AssetStatusDTO>> GetAssetsWithStatusAsync()
        {
            return await _DtoizeAssetsWithStatus(_context.Assets).ToListAsync();
        }

        public async Task<IEnumerable<AssetStatusDTO>> GetAssetsWithStatusFilterByStatus(ProcessedStatus status)
        {
            return await _DtoizeAssetsWithStatus(_context.Assets.Where(a=> a.AssetLiveStatus!= null && a.AssetLiveStatus.ProcessedStatus == status)).ToListAsync();
        }
    }
}