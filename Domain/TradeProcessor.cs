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

        private IEnumerable<Traderecord> ParseRecordLines(IEnumerable<string> records)
        {
            int lineNumber = 1; 
            IEnumerable<TradeRecord> validatedTradeRecords = new List<TradeRecord>();
            foreach (string unvalidatedTradeRecord in records)
            {
                //splitting along the comma (we assume that the fields are separated using commas)
                var recordFields = unvalidatedTradeRecord.Split(",").ToList();
                
                if(recordFields.Count != 6){
                     Console.WriteLine($"Malformed record line on line number {lineNumber}"); 
                     continue;
                }
                if(recordFields[0].Length != 6){
                    Console.WriteLine($"Malformed Id on line number {lineNumber}");
                    continue;
                }
                if(recordFields[1].Length > 20){
                    Console.WriteLine($"Malformed ClientName on line number {lineNumber}");
                    continue;
                }
                if(recordFields[2].Length > 10){
                    Console.WriteLine($"Malformed ItemName on line number {lineNumber}");
                    continue;
                }
                if(!int.TryParse(recordFields[3], out int _)){
                    Console.WriteLine($"Malformed Quantity value on line number {lineNumber}");
                    continue;
                }
                if(recordFields[4].Length <= 3){
                    Console.WriteLine($"Malformed UnitPrice currency on line number {lineNumber}");
                    continue;
                }
                if(recordFields[5].Length <= 3) {
                    Console.WriteLine($"Malformed Total Price currency on line number {lineNumber}");
                    continue;
                }

                var id = TransactionId.FromString(recordFields[0]);
                var clientName = ClientName.FromString(recordFields[1]);
                var itemName = ItemName.FromString(recordFields[2]);
                var quantity = ItemQuantity.FromInt(int.Parse(recordFields[3]));

                Amount amount;
                var currency = ParseMoney(recordFields[4], out amount);
                var unitPrice = Money.CreateMoney(amount, currency);

                currency = ParseMoney(recordFields[5], out amount);
                var totalPrice = Money.CreateMoney(amount, currency);


                var tradeRecord = TradeRecord.CreateTradeRecord(id, clientName, itemName, quantity, unitPrice, totalPrice);

                validatedTradeRecords.Add(tradeRecord);
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
        {
            Traderecord databaseObject = new Traderecord{
                Id = trade.Id.ToString(),
                ClientName = trade.ClientName.ToString(),
                ItemName = trade.ItemName.ToString(),
                ItemQuantity = int.Parse(trade.ItemQuantity.ToString()),
                UnitPrice = trade.UnitPrice.ToString(),
                TotalPrice = trade.TotalPrice.ToString()
            };

            return databaseObject;
        }

        private static Currency ParseMoney(string curr, out Amount amount){
            string currency = curr.Substring(0,3);
            string value = curr.Substring(3);

            amount = Amount.FromDecimal(decimal.Parse(value));
            return (Currency)Enum.Parse(typeof(Currency), currency);
        }
    }
}