using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ValueObjects;
using Domain.Models;
using Domain.Services;

namespace Domain
{
    public class TradeProcessor
    {
        private readonly IDataProvider reader;
        private readonly IParser parser;
        private readonly IRepository repository;

        public TradeProcessor(IDataProvider reader, IParser parser, IRepository repository)
        {
            this.reader = reader;
            this.parser = parser;
            this.repository = repository;
        }


        public void ProcessTrades()
        {
            var unvalidatedRecords = reader.Read();
            var validatedRecords = parser.Parse(unvalidatedRecords);
            repository.Save(validatedRecords);
        }
            
    }
}