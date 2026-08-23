using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Services
{
    public interface IIRonGridLogger
    {
        public void Debug(string serviceName, string eventDetails);
        public void Warning(string serviceName, string eventDetails);
        public void Error(string serviceName, string eventDetails);
    }
}