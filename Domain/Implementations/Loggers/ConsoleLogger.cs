using System;
using Domain.Services;

namespace Domain.Implementations.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string logMessage)
            =>  Console.WriteLine(logMessage);
    }
}