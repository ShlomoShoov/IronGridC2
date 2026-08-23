using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Producer.Services;

namespace Producer.Services
{
    public class ConsoleLogger : IIRonGridLogger
    {
        public void Debug(string serviceName, string eventDetails)
        {
            System.Console.WriteLine($"Debug | {serviceName} | {eventDetails}");
        }
        public void Warning(string serviceName, string eventDetails)
        {
            System.Console.WriteLine($"Warning | {serviceName} | {eventDetails}");

        }
        public void Error(string serviceName, string eventDetails)
        {
            System.Console.WriteLine($"Error | {serviceName} | {eventDetails}");

        }
    }
}