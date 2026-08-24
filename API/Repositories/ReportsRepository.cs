using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consumer.DAL;

namespace API.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private IronGridDbContext _context;

        public ReportsRepository(IronGridDbContext context)
        {
            _context = context;
        }
    }
}