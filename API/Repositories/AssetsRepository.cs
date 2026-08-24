using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTOs.AssetsDTOs;
using Consumer.DAL;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AssetsRepository : IAssetsRepository
    {
        private IronGridDbContext _context;

        public AssetsRepository(IronGridDbContext context)
        {
            _context = context;
        }
        private AssetDTO DtoizeAsset(Asset asset)
        {
            return new AssetDTO
            {
                Id = asset.Id,
                UnitId = asset.UnitId,
                AssetSerial = asset.AssetSerial,
                AssetType = asset.AssetType
            };
        }
        public async Task<AssetDTO?> GetAssetByIdAsync(int id)
        {
            Asset? asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == id);
            if (asset == null)
            {
                return null;
            }
            return DtoizeAsset(asset);
        }

        public async Task CreateUnitAsync(CreateUnitDto unitDto)
        {
            Unit createdUnit = new Unit
            {
              Sector = unitDto.Sector,
              UnitName = unitDto.UnitName  
            };
            _context.Units.Add(createdUnit);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAssetAsync(int id, UpdateAssetDTO updatedAsset)
        {
            Asset? asset = await _context.Assets.FirstOrDefaultAsync(a=> a.Id == id);
            if (asset == null)
            {
                return false;
            }

            asset.UnitId = updatedAsset.UnitId;
            asset.AssetType = updatedAsset.AssetType;
            asset.AssetSerial = updatedAsset.AssetSerial;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAssetAsync(int id)
        {
            return await _context.Assets.Where(a=> a.Id == id).ExecuteDeleteAsync() > 0;
        }



        
    }
}