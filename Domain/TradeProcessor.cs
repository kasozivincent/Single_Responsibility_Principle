using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ValueObjects;
using Domain.Models;
using Domain.Services;

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
    public class TradeProcessor<T>
    {
        private readonly IDataProvider provider;
        private readonly IParser<T> parser;
        private readonly IRepository<T> repository;

        public TradeProcessor(IDataProvider provider, IParser<T> parser, IRepository<T> repository)
        {
            this.provider = provider;
            this.parser = parser;
            this.repository = repository;
        }

        private void Log(string logMessage)
        {
            Console.WriteLine(logMessage);
        }
            

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


        private void StoreRecords(IEnumerable<Traderecord> records)
        {
            srpContext dbcontext = new srpContext();
            dbcontext.Traderecords.AddRange(records);
            dbcontext.SaveChanges();
        }

        public void ProcessTrades()
            => repository.Save(parser.Parse(provider.Read()));

       

        private static Currency ParseMoney(string curr, out Amount amount){
            string currency = curr.Substring(0,3);
            string value = curr.Substring(3);

            amount = Amount.FromDecimal(decimal.Parse(value));
            return (Currency)Enum.Parse(typeof(Currency), currency);
        }
    }
}