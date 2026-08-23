using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.DAL;
using Consumer.Services;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Repositories
{
    public class AssetLiveStatusRepository
    {
        private IronGridDbContext _context;
        private IIRonGridLogger _logger;
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
    }
}