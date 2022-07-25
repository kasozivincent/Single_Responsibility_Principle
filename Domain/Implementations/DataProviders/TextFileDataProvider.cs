using System.Collections.Generic;
using System.IO;
using Domain.Services;
using System;

namespace Domain.Implementations.DataProviders
{
    public class TextFileDataProvider : IDataProvider
    {
        private string fileName;

        public TextFileDataProvider(string fileName)
            => this.fileName = fileName;
        public IEnumerable<string> Read()
            => File.ReadLines(fileName);
    }
}
