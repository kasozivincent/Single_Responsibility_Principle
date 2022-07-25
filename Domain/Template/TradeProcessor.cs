using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Models;
using Domain.Services;

namespace Domain.Template
{
    public abstract class TradeProcessor
    {
            
        public void ProcessTrades()
        {
            var unvalidatedRecords = Read();
            var validatedRecords = Parse(unvalidatedRecords);
            Save(validatedRecords);
        }

        protected abstract IEnumerable<string> Read();
        protected abstract IEnumerable<Traderecord> Parse(IEnumerable<string> records);
        protected abstract void Save(IEnumerable<Traderecord> records);
    }

    public sealed class Foo : TradeProcessor
    {
        private readonly ILogger logger;
        private readonly string fileName;
        private readonly IMapper mapper;

        public Foo(ILogger logger, IMapper mapper, string fileName)
        {
            this.logger = logger;
            this.fileName = fileName;
            this.mapper = mapper;
        }
          

         private  Traderecord Transformer(TradeRecord trade)
            => new Traderecord{
                Id = trade.Id.ToString(),
                ClientName = trade.ClientName.ToString(),
                ItemName = trade.ItemName.ToString(),
                ItemQuantity = int.Parse(trade.ItemQuantity.ToString()),
                UnitPrice = trade.UnitPrice.ToString(),
                TotalPrice = trade.TotalPrice.ToString()
            };
            

        protected override IEnumerable<Traderecord> Parse(IEnumerable<string> records)
        {
            int lineNumber = 1; 
            ICollection<TradeRecord> validatedTradeRecords = new List<TradeRecord>();
            foreach (string unvalidatedTradeRecord in records)
            {
                var recordFields = unvalidatedTradeRecord.Split(",").ToList();
                
                if(recordFields.Count != 6){
                    logger.Log($"Malformed record line on line number {lineNumber}"); 
                     continue;
                }
                if(recordFields[0].Length != 6){
                    logger.Log($"Malformed Id on line number {lineNumber}");
                    continue;
                }
                if(recordFields[1].Length > 20){
                    logger.Log($"Malformed ClientName on line number {lineNumber}");
                    continue;
                }
                if(recordFields[2].Length > 10){
                    logger.Log($"Malformed ItemName on line number {lineNumber}");
                    continue;
                }
                if(!int.TryParse(recordFields[3], out int _)){
                    logger.Log($"Malformed Quantity value on line number {lineNumber}");
                    continue;
                }
                if(recordFields[4].Length <= 3){
                    logger.Log($"Malformed UnitPrice currency on line number {lineNumber}");
                    continue;
                }
                if(recordFields[5].Length <= 3) {
                    logger.Log($"Malformed Total Price currency on line number {lineNumber}");
                    continue;
                }

                validatedTradeRecords.Add(mapper.Map(recordFields));
                lineNumber++;

            }

            return validatedTradeRecords.Select(Transformer).ToList();
        }

        protected override IEnumerable<string> Read()
            => File.ReadLines(fileName);

        protected override void Save(IEnumerable<Traderecord> records)
        {
            throw new System.NotImplementedException();
        }
    }
}