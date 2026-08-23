using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.DAL;
using Consumer.Models;
using Consumer.Services;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Repositories
{
    public class AssetLiveStatusRepository
    {
        private IronGridDbContext _context;
        private IIRonGridLogger _logger;
        private string _serviceName = "AssetLiveStatus Repository";
        public AssetLiveStatusRepository(IronGridDbContext context, IIRonGridLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitAsync(string seedRaw)
        {
            await _context.Database.ExecuteSqlRawAsync(seedRaw);
            await _context.SaveChangesAsync();
        }

        public async Task AddAssetLiveStatus(AssetLiveStatus assetLiveStatus)
        {
            Asset? asset = await _context.Assets.Include(a=> a.AssetLiveStatus).FirstOrDefaultAsync(a => a.Id == assetLiveStatus.AssetId);
            if (asset == null)
            {
                _logger.Error(_serviceName, $"Try to add new Asset live status but There is no Asset id: {assetLiveStatus.AssetId}");
                return;
            }

            if (asset.AssetLiveStatus == null)
            {
                _context.AssetLiveStatuses.Add(assetLiveStatus);
                await _context.SaveChangesAsync();
                _logger.Debug(_serviceName, $"First Asset status for {assetLiveStatus.AssetId} Entering to the data base new status line: {assetLiveStatus.Id}");
            }

            else
            {
                asset.AssetLiveStatus.AssetType = assetLiveStatus.AssetType;
                asset.AssetLiveStatus.IsVerified = assetLiveStatus.IsVerified;
                asset.AssetLiveStatus.ProcessedStatus = assetLiveStatus.ProcessedStatus;
                asset.AssetLiveStatus.RawValue = assetLiveStatus.RawValue;
                await _context.SaveChangesAsync();
                _logger.Debug(_serviceName, $"there is existing status for {assetLiveStatus.AssetId} replacing data ");


            }
        }
    }
}