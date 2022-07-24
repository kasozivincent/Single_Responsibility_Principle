using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ValueObjects;
using Domain.Models;

/*
    Problems with this God class.
    1. Reads the input data
    2. Parses the data to check if it obeys the rules
    3. Logging errors
    4. Mapping input fields to domain objects
    5. Storing the data to a database
*/
namespace Domain
{
    public class TradeProcessor
    {
        private IEnumerable<string> ReadData(string fileName)
            => File.ReadLines(fileName);

        private void Log(string logMessage)
            => Console.WriteLine(logMessage);

        private TradeRecord DomainMapper(List<string> fields)
        {
            var id = TransactionId.FromString(fields[0]);
                var clientName = ClientName.FromString(fields[1]);
                var itemName = ItemName.FromString(fields[2]);
                var quantity = ItemQuantity.FromInt(int.Parse(fields[3]));

                Amount amount;
                var currency = ParseMoney(fields[4], out amount);
                var unitPrice = Money.CreateMoney(amount, currency);

                currency = ParseMoney(fields[5], out amount);
                var totalPrice = Money.CreateMoney(amount, currency);
                return TradeRecord.CreateTradeRecord(id, clientName, itemName, quantity, unitPrice, totalPrice);
                
        }

        private IEnumerable<Traderecord> ParseRecordLines(IEnumerable<string> records)
        {
            int lineNumber = 1; 
            ICollection<TradeRecord> validatedTradeRecords = new List<TradeRecord>();
            foreach (string unvalidatedTradeRecord in records)
            {
                //splitting along the comma (we assume that the fields are separated using commas)
                var recordFields = unvalidatedTradeRecord.Split(",").ToList();
                
                if(recordFields.Count != 6){
                     Log($"Malformed record line on line number {lineNumber}"); 
                     continue;
                }
                if(recordFields[0].Length != 6){
                    Log($"Malformed Id on line number {lineNumber}");
                    continue;
                }
                if(recordFields[1].Length > 20){
                    Log($"Malformed ClientName on line number {lineNumber}");
                    continue;
                }
                if(recordFields[2].Length > 10){
                    Log($"Malformed ItemName on line number {lineNumber}");
                    continue;
                }
                if(!int.TryParse(recordFields[3], out int _)){
                    Log($"Malformed Quantity value on line number {lineNumber}");
                    continue;
                }
                if(recordFields[4].Length <= 3){
                    Log($"Malformed UnitPrice currency on line number {lineNumber}");
                    continue;
                }
                if(recordFields[5].Length <= 3) {
                    Log($"Malformed Total Price currency on line number {lineNumber}");
                    continue;
                }

                validatedTradeRecords.Add(DomainMapper(recordFields));
                lineNumber++;
            }

            return validatedTradeRecords.Select(Transformer);
        }

        private void StoreRecords(IEnumerable<Traderecord> records)
        {
            srpContext dbcontext = new srpContext();
            dbcontext.Traderecords.AddRange(records);
            dbcontext.SaveChanges();
        }

        public void ProcessTrades(string filename)
        {
            var record = ReadData(filename);
            var parsedRecords = ParseRecordLines(record);
            StoreRecords(parsedRecords);
        }

        private static Traderecord Transformer(TradeRecord trade)
            => new Traderecord{
                Id = trade.Id.ToString(),
                ClientName = trade.ClientName.ToString(),
                ItemName = trade.ItemName.ToString(),
                ItemQuantity = int.Parse(trade.ItemQuantity.ToString()),
                UnitPrice = trade.UnitPrice.ToString(),
                TotalPrice = trade.TotalPrice.ToString()
            };

        private static Currency ParseMoney(string curr, out Amount amount){
            string currency = curr.Substring(0,3);
            string value = curr.Substring(3);

            amount = Amount.FromDecimal(decimal.Parse(value));
            return (Currency)Enum.Parse(typeof(Currency), currency);
        }
    }
}