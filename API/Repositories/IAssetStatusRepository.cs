using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTOs.AssetsStatusDTOs;

namespace API.Repositories
{
    public interface IAssetStatusRepository
    {
        public  Task<IEnumerable<AssetStatusDTO>> GetAssetsWithStatusAsync();
        public  Task<IEnumerable<AssetStatusDTO>> GetAssetsWithStatusFilterByStatus(ProcessedStatus status);
        public  Task<AssetStatusDTO?> GetAssetStatusByIdAsync(int id);




    }
}