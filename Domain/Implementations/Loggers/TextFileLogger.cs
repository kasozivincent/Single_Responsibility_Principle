using System.Collections.Generic;
using System.IO;
using Domain.Services;

namespace Domain.Implementations.Loggers
{
    public class TextFileLogger : ILogger
    {
        private readonly string fileName;

        public TextFileLogger(string fileName)
            => this.fileName = fileName;
        public void Log(string logMessage)
            => File.AppendAllLines(fileName, new List<string>(){logMessage});
    }
}