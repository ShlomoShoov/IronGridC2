using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTOs.AssetsDTOs;

namespace API.Repositories
{
    public interface IAssetsRepository
    {
        public  Task<AssetDTO?> GetAssetByIdAsync(int id);
        public  Task CreateUnitAsync(CreateUnitDto unitDto);
        public Task<AssetDTO?> UpdateAssetAsync(int id, UpdateAssetDTO updatedAsset);
        public  Task<bool> DeleteAssetAsync(int id);



    }
}