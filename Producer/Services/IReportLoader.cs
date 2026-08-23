using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Producer.Models;

namespace Producer.Services
{
    public interface IReportLoader
    {
        public IEnumerator<Report> LoadReports();
    }
}