using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Services
{
    public class ConsoleLogger : IIRonGridLogger
    {
        public void Debug(string serviceName, string eventDetails)
        {
            _DisplayServiceName(serviceName);
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"| Debug | {eventDetails}");
            Console.ResetColor();

        }
        public void Warning(string serviceName, string eventDetails)
        {
            _DisplayServiceName(serviceName);
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine($"| Waring | {eventDetails}");
            Console.ResetColor();

        }
        public void Error(string serviceName, string eventDetails)
        {
            _DisplayServiceName(serviceName);
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"| Error | {eventDetails}");
            Console.ResetColor();

        }
        private void _DisplayServiceName(string serviceName)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.Write(serviceName);
            Console.ResetColor();
        }
    }
}