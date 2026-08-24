using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.DAL;

namespace API.Repositories
{
    public class AssetStatusRepository : IAssetStatusRepository
    {
        private IronGridDbContext _context;

        public AssetStatusRepository(IronGridDbContext context)
        {
            _context = context;
        }
    }
}