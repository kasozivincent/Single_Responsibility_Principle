using System.Collections.Generic;
using System.IO;
using Domain.Services;
using System;

namespace Domain.Implementations
{
     public class ConsoleDataProvider : IDataProvider
    {
        public IEnumerable<string> Read()
        {
            var input = Console.ReadLine();
            var lines = new List<string>();
            lines.Add(input);
            return lines;
        }
            
    }
}