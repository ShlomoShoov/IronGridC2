using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTOs.AssetsStatusDTOs;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace API.Controllers
{
    [ApiController]
    [Route("api/assets-status")]
    public class AssetsStatusController : ControllerBase
    {
        private IAssetStatusRepository _repository;
        private readonly IDatabase _redis;

        public AssetsStatusController(IAssetStatusRepository repository, IConnectionMultiplexer muxer)
        {
            _repository = repository;
            _redis = muxer.GetDatabase();
        }

        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetStatusDTO>>> GetAssetsWithStatusAsync(string? status)
        {
            if(status == null)
            {
                return Ok(await _repository.GetAssetsWithStatusAsync());
            }
            else if(!Enum.TryParse<ProcessedStatus>(status, out ProcessedStatus processedStatus))
            {
                return Ok(new List<AssetStatusDTO> ());
            }
            else
            {
                return Ok(await _repository.GetAssetsWithStatusFilterByStatus(processedStatus));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetStatusDTO>> GetAssetStatusByIdAsync(int id)
        {
            TimeSpan TTL = TimeSpan.FromMinutes(5);
            string keyName = $"AssetStatus:{id}";

            string? jsonResult = await _redis.StringGetAsync(keyName);

            if (string.IsNullOrEmpty(jsonResult))
            {

                System.Console.WriteLine($"Not found any Data in redis on kay: {keyName}");
                AssetStatusDTO? asset = await _repository.GetAssetStatusByIdAsync(id);

                if (asset == null)
                {
                    System.Console.WriteLine($"Don't Found in Data Base either!");
                    return NotFound();
                }
                
                string dataBaseResult = JsonSerializer.Serialize(asset);
                var setTask = _redis.StringSetAsync(keyName,dataBaseResult);
                var expireTask = _redis.KeyExpireAsync(keyName,TTL);
                await Task.WhenAll(setTask, expireTask);
                System.Console.WriteLine($"Enter data to redis from database! row data: {dataBaseResult} | key: {keyName}");
                return Ok(asset);
            }

            System.Console.WriteLine($"Ho Ho! Got New data from redis!! raw data: {jsonResult} | key: {keyName}");

            AssetStatusDTO assetFromRedis = JsonSerializer.Deserialize<AssetStatusDTO>(jsonResult)!;
            return Ok(assetFromRedis);
            

        }





    }
}